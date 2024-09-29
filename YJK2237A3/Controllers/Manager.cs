using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using YJK2237A3.Data;
using YJK2237A3.Models;
using System.Diagnostics;


// ************************************************************************************
// WEB524 Project Template V1 == 2237-a868c62e-10a3-4862-8725-4ae4a5d50310
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace YJK2237A3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {

                // Albums
                cfg.CreateMap<Album, AlbumBaseViewModel>();

                // MediaTypes
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();

                // Artist
                cfg.CreateMap<Artist, ArtistBaseViewModel>();

                // Tracks
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>()
                    .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album.Title))
                    .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Album.Artist.Name))
                    .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MediaType));
                cfg.CreateMap<TrackAddFormViewModel, TrackAddViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>()
                    .ForMember(dest => dest.TrackId, opt => opt.Ignore())
                    .ForMember(dest => dest.MediaTypeId, opt => opt.MapFrom(src => src.SelectedMediaTypeId))
                    .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.SelectedAlbumId));

                //PlayLists
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>()
                    .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks))
                    .ForMember(dest => dest.PlaylistId, opt => opt.MapFrom(src=> src.PlaylistId));
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();
                cfg.CreateMap<PlaylistEditTracksFormViewModel, PlaylistEditTracksViewModel>();
                cfg.CreateMap<PlaylistEditTracksViewModel, Playlist>();

            })

            {

            };
            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        //Albums
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = from t in ds.Albums
                         orderby t.Title
                         select t;
            return mapper.Map<IEnumerable<AlbumBaseViewModel>>(albums);
        }

        //MediaTypes
        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var mediaTypes = from t in ds.MediaTypes
                         orderby t.Name
                         select t;
            return mapper.Map<IEnumerable<MediaTypeBaseViewModel>>(mediaTypes);
        }

        //Artists
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artists = from t in ds.Artists
                             orderby t.Name
                             select t;
            return mapper.Map<IEnumerable<ArtistBaseViewModel>>(artists);
        }

        //Tracks
        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            var tracks = ds.Tracks
                .Include(t => t.Album)
                .Include(t => t.Album.Artist)
                .Include(t => t.MediaType)
                .OrderBy(t => t.Name);
            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(tracks);
        }

        public TrackWithDetailViewModel TrackGetByIdWithDetail(int id)
        {
            var track = ds.Tracks
                .Include(t=>t.MediaType)
                .Include(t=>t.Album)
                .Include(t=>t.Album.Artist)
                .SingleOrDefault(t => t.TrackId == id);

            return mapper.Map<TrackWithDetailViewModel>(track);
        }

        public TrackBaseViewModel TrackAdd(TrackAddFormViewModel track)
        {
            var trackToAdd = mapper.Map<TrackAddViewModel>(track);
            var addedTrack = mapper.Map<Track>(trackToAdd);
            ds.Tracks.Add(addedTrack);
            ds.SaveChanges();

            return mapper.Map<TrackBaseViewModel>(addedTrack);
        }

        //Playlists
        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var playlists = from p in ds.Playlists
                            .Include("Tracks")
                            orderby p.Name
                            select p;
            return mapper.Map<IEnumerable<PlaylistBaseViewModel>>(playlists);
        }

        public PlaylistBaseViewModel PlaylistGetById(int id)
        {
            var playlists = ds.Playlists
                .Include(t => t.Tracks)
                .SingleOrDefault(t => t.PlaylistId == id);

            return mapper.Map<PlaylistBaseViewModel>(playlists);
        }

        public PlaylistBaseViewModel PlaylistEditTracks(PlaylistEditTracksFormViewModel model)
        {
            Debug.WriteLine("it's working");
            var playlist = ds.Playlists
                .Include(p => p.Tracks)
                .FirstOrDefault(p => p.PlaylistId == model.PlaylistId);
            if (playlist == null) 
            {
                Debug.WriteLine($"Playlist not found with id: {model.PlaylistId}");
                return null; 
            }

            playlist.Tracks.Clear();
            if(model.SelectedTrackIds != null)
            {
                foreach (var trackId in model.SelectedTrackIds)
                {
                    var selectedTrack = ds.Tracks.FirstOrDefault(t => t.TrackId == trackId);
                    if (selectedTrack != null)
                    {
                        playlist.Tracks.Add(selectedTrack);
                    }
                    else
                    {
                        Debug.WriteLine("There is no tracks in selectedTrack");
                    }

                }
                
            }
            ds.SaveChanges();


            return mapper.Map<PlaylistBaseViewModel>(playlist);
        }

    }
}