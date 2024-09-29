using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using YJK2237A2.Data;
using YJK2237A2.Models;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-02af8659-fe17-48ed-b210-98f70d656160
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace YJK2237A2.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
         
                // Tracks
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>()
                    .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album.Title))
                    .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

                //Invoices
                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>()
                   .ForMember(dest => dest.TrackName, opt => opt.MapFrom(src => src.Track.Name))
                   .ForMember(dest => dest.Composer, opt => opt.MapFrom(src => src.Track.Composer))
                   .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Track.Album.Title))
                   .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Track.Album.Artist.Name))
                   .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Track.Genre.Name))
                   .ForMember(dest => dest.MediaTypeName, opt => opt.MapFrom(src => src.Track.MediaType.Name));

                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>()
                   .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                   .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.Customer.LastName))
                   .ForMember(dest => dest.CustomerState, opt => opt.MapFrom(src => src.Customer.State))
                   .ForMember(dest => dest.CustomerCountry, opt => opt.MapFrom(src => src.Customer.Country))
                   .ForMember(dest => dest.CustomerEmployeeFirstName, opt => opt.MapFrom(src => src.Customer.Employee.FirstName))
                   .ForMember(dest => dest.CustomerEmployeeLastName, opt => opt.MapFrom(src => src.Customer.Employee.LastName))
                   .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLines));
            });

            mapper = config.CreateMapper();

            ds.Configuration.ProxyCreationEnabled = false;

            ds.Configuration.LazyLoadingEnabled = false;
        }


        //Tracks
        public IEnumerable<TrackWithDetailViewModel> TrackGetAll()
        {
            var tracks = ds.Tracks
                .Include(t => t.Album)
                .Include(t => t.Genre)
                .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(tracks);
        }
        public IEnumerable<TrackWithDetailViewModel> TrackGetBluesJazz()
        {
            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(ds.Tracks
                .Include(t => t.Album)
                .Include(t => t.Genre)
                .Where(t => t.GenreId == 2 || t.GenreId == 6)
                .OrderBy(t => t.Genre.Name)
                .ThenBy(t => t.Name));
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetCantrellStaley()
        {
            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(ds.Tracks
                .Include(t => t.Album)
                .Include(t => t.Genre)
                .Where(t => t.Composer.Contains("Jerry Cantrell") && t.Composer.Contains("Layne Staley"))
                .OrderBy(t => t.Composer)
                .ThenBy(t => t.Name));
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetTop50Longest()
        {
            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(ds.Tracks
                .Include(t => t.Album)
                .Include(t => t.Genre)
                .OrderByDescending(t => t.Milliseconds)
                .Take(50)
                .OrderBy(t => t.Name));
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetTop50Smallest()
        {
            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(ds.Tracks
                .Include(t => t.Album)
                .Include(t => t.Genre)
                .OrderBy(t => t.Bytes)
                .Take(50)
                .OrderBy(t => t.Name));
        }


        //Invoices
        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        {
            var invoices = ds.Invoices
                .OrderByDescending(i => i.InvoiceDate);

            return mapper.Map<IEnumerable<InvoiceBaseViewModel>>(invoices);
        }

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int id)
        {
            var invoice = ds.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceLines.Select(il => il.Track.Album))
                .Include(i => i.InvoiceLines.Select(il => il.Track.Album.Artist))
                .Include(i => i.InvoiceLines.Select(il => il.Track.Genre))
                .Include(i => i.InvoiceLines.Select(il => il.Track.MediaType))
                .Include("Customer.Employee")
                .SingleOrDefault(i => i.InvoiceId == id);

            return mapper.Map<InvoiceWithDetailViewModel>(invoice);
        }

    }
}