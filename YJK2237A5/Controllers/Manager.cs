using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using YJK2237A5.Models;
using System.Data.Entity;

// ************************************************************************************
// WEB524 Project Template V2 == 2237-0cc2b32a-c577-419b-ad51-53932dc0c4c6
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace YJK2237A5.Controllers
{
    public class Manager
    {

        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                
                //Genre
                cfg.CreateMap<Data.Genre, GenreBaseViewModel>();

                //Actor
                cfg.CreateMap<Data.Actor, ActorBaseViewModel>();
                cfg.CreateMap<ActorAddFormViewModel, ActorAddViewModel>();
                cfg.CreateMap<ActorAddViewModel, Data.Actor>();
                cfg.CreateMap<Data.Actor, ActorWithDetailViewModel>()
                     .ForMember(dest => dest.Shows, opt => opt.MapFrom(src => src.Shows));
                cfg.CreateMap<ActorBaseViewModel, Data.Actor>();

                //Show
                cfg.CreateMap<Data.Show, ShowBaseViewModel>();
                cfg.CreateMap<ShowAddFormViewModel, ShowWithDetailViewModel>()
                .AfterMap((src, dest, ctx) => {
                    dest.Episodes = new List<EpisodeBaseViewModel>();
                    if (src.SelectedActorIds != null && src.SelectedActorIds.Any())
                    {
                        dest.Actors = src.SelectedActorIds
                            .Select(id => ctx.Mapper.Map<ActorBaseViewModel>(ds.Actors.Find(id)))
                            .Where(actor => actor != null);
                    }
                    else
                    {
                        dest.Actors = new List<ActorBaseViewModel>();
                    }
                });
                cfg.CreateMap<Data.Show, ShowWithDetailViewModel>()
                    .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
                    .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes));
                cfg.CreateMap<ShowWithDetailViewModel, Data.Show>()
                    .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
                    .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes));
                cfg.CreateMap<ShowBaseViewModel, Data.Show>();

                //Episode
                cfg.CreateMap<Data.Episode, EpisodeBaseViewModel>()
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Show));
                cfg.CreateMap<EpisodeAddFormViewModel, EpisodeAddViewModel>();
                cfg.CreateMap<EpisodeAddViewModel, Data.Episode>()
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Show));
                cfg.CreateMap<Data.Episode, EpisodeWithDetailViewModel>()
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Show));
                cfg.CreateMap<EpisodeBaseViewModel, Data.Episode>()
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Show));
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        //Genre
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var genres = ds.Genres
                .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<GenreBaseViewModel>>(genres);
        }

        //Actor
        public IEnumerable<ActorBaseViewModel> ActorGetAll()
        {
            var actors = ds.Actors
                .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<ActorBaseViewModel>>(actors);
        }

        public ActorWithDetailViewModel ActorGetByIdWithDetail(int id)
        {
            var actor = ds.Actors
                .Include(a=> a.Shows)
                .SingleOrDefault(a => a.Id == id);

            return mapper.Map<ActorWithDetailViewModel>(actor);
        }
        public ActorBaseViewModel ActorAdd(ActorAddFormViewModel actor)
        {
            var actorToAdd = mapper.Map<ActorAddViewModel>(actor);
            var addedActor = mapper.Map<Data.Actor>(actorToAdd);
            ds.Actors.Add(addedActor);
            ds.SaveChanges();

            return mapper.Map<ActorBaseViewModel>(addedActor);
        }


        //Show
        public IEnumerable<ShowBaseViewModel> ShowGetAll()
        {
            var shows = ds.Shows
                .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<ShowBaseViewModel>>(shows);
        }

        public ShowWithDetailViewModel ShowGetByIdWithDetail(int id)
        {
            var show = ds.Shows
                .Include(s => s.Actors)
                .Include(s => s.Episodes) 
                .SingleOrDefault(s => s.Id == id);

            return mapper.Map<ShowWithDetailViewModel>(show);
        }

        public ShowBaseViewModel ShowGetById(int id)
        {
            var show = ds.Shows
                .SingleOrDefault(s => s.Id == id);

            return mapper.Map<ShowBaseViewModel>(show);
        }

        public ShowWithDetailViewModel ShowAdd(ShowAddFormViewModel show)
        {
            System.Diagnostics.Debug.WriteLine("#######INside Show ADD");
            var showToAdd = mapper.Map<ShowWithDetailViewModel>(show);
            System.Diagnostics.Debug.WriteLine("#######after mapping ShowWithDetailViewModel");
            var addedShow = mapper.Map<Data.Show>(showToAdd);
            System.Diagnostics.Debug.WriteLine("#######after mapping addedShow");
            if (show.SelectedActorIds != null)
            {
                addedShow.Actors = new List<Data.Actor>();
                foreach (var id in show.SelectedActorIds)
                {
                    var actor = ds.Actors.Find(id);
                    if(actor != null)
                    {
                        addedShow.Actors.Add(actor);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("#######Finishing add actors");
            addedShow.Genre = show.Genre;
            System.Diagnostics.Debug.WriteLine("#######Adding Genre");
            addedShow.Coordinator = User.Name;
            System.Diagnostics.Debug.WriteLine("#######Try to save added show");
            ds.Shows.Add(addedShow);
            ds.SaveChanges();

            return mapper.Map<ShowWithDetailViewModel>(addedShow);
        }
        //Episode
        public IEnumerable<EpisodeBaseViewModel> EpisodeGetAll()
        {
            var episodes = ds.Episodes
                .Include(e => e.Show)
                .OrderBy(e => e.Show.Name)
                .ThenBy(e => e.SeasonNumber)
                .ThenBy(e => e.EpisodeNumber);

            return mapper.Map<IEnumerable<EpisodeBaseViewModel>>(episodes);
        }
        public EpisodeWithDetailViewModel EpisodeGetByIdWithDetail(int id)
        {
            var episode = ds.Episodes
                .Include(e => e.Show) 
                .SingleOrDefault(e => e.Id == id);

            return mapper.Map<EpisodeWithDetailViewModel>(episode);
        }
        public EpisodeBaseViewModel EpisodeAdd(EpisodeAddFormViewModel episode)
        {
            var episodeToAdd = mapper.Map<EpisodeAddViewModel>(episode);
            var addedEpisode = mapper.Map<Data.Episode>(episodeToAdd);
            addedEpisode.Clerk = User.Name;
            ds.Episodes.Add(addedEpisode);
            ds.SaveChanges();

            return mapper.Map<EpisodeBaseViewModel>(addedEpisode);
        }


        //Video
        public EpisodeVideoViewModel EpisodeVideoGetById(int id)
        {
            var episode = EpisodeGetByIdWithDetail(id);

            if (episode != null)
            {
                return new EpisodeVideoViewModel
                {
                    Id = episode.Id
                };
            }
            return null;
        }


        // *** Add your methods ABOVE this line **

        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // You can write one method or many methods but remember to
        // check for existing data first.  You will call this/these method(s)
        // from a controller action.

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new Data.RoleClaim { Name = "Admin" });
                ds.RoleClaims.Add(new Data.RoleClaim { Name = "Excutive" });
                ds.RoleClaims.Add(new Data.RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new Data.RoleClaim { Name = "Clerk" });

                ds.SaveChanges();
                done = true;
            }
            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoadGenres()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Genres ***
            if (ds.Genres.Count() == 0)
            {
                ds.Genres.Add(new Data.Genre { Name = "Action" });
                ds.Genres.Add(new Data.Genre { Name = "Animation" });
                ds.Genres.Add(new Data.Genre { Name = "Comedy" });
                ds.Genres.Add(new Data.Genre { Name = "Documentary" });
                ds.Genres.Add(new Data.Genre { Name = "Drama" });
                ds.Genres.Add(new Data.Genre { Name = "Fantasy" });
                ds.Genres.Add(new Data.Genre { Name = "Family" });
                ds.Genres.Add(new Data.Genre { Name = "Mystery" });
                ds.Genres.Add(new Data.Genre { Name = "Sci-Fi" });
                ds.Genres.Add(new Data.Genre { Name = "Romance" });

                ds.SaveChanges();
                done = true;
            }
            return done;
        }
        public bool LoadActors()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Actors ***
            if (ds.Actors.Count() == 0)
            {
                ds.Actors.Add(new Data.Actor
                {
                    Name = "Jennifer Aniston",
                    BirthDate = new DateTime(1969, 2, 11),
                    Height = 1.64,
                    ImageUrl = "https://t0.gstatic.com/licensed-image?q=tbn:ANd9GcTyLqLQBAt_AQWl2SAOCGnO9H2KdCOpMama6C6J3cP5-KKsqeOygSQ9scOwtSIDao9g",
                    Executive = "exec@example.com"
                });

                ds.Actors.Add(new Data.Actor
                {
                    Name = "Anya Taylor-Joy",
                    BirthDate = new DateTime(1996, 4, 16),
                    Height = 1.73,
                    ImageUrl = "https://t1.gstatic.com/licensed-image?q=tbn:ANd9GcQ2PhEXCGYXRVnkalObc7slqPdro307S7I7cjzQay6VUVzVxN7-aNv4hSt-fKbJ9eEO",
                    Executive = "exec@example.com"
                });

                ds.Actors.Add(new Data.Actor
                {
                    Name = "Gong Yoo",
                    BirthDate = new DateTime(1979, 7, 10),
                    Height = 1.85,
                    ImageUrl = "https://i.namu.wiki/i/WQMybkD5Md63lcByoByQ63EXt-tup8WnjMjxKs_Jg5kymkkdeLZzmdXagjKzW6AzVvjhPUeJUSZJ2sRSMCj1XTNf3lFd_NrTwI36iLv-ItFLwjM70gBGsK9IAneDZ1MsrzgwDylgEhOLnH762SlyxQ.webp",
                    Executive = "exec@example.com"
                });
                ds.SaveChanges();
                done = true;
            }

            return done;
        }
        public bool LoadShows()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Showss ***
            if (ds.Shows.Count() == 0)
            {
                var gongyoo = ds.Actors.SingleOrDefault(a => a.Name == "Gong Yoo");
                ds.Shows.Add(new Data.Show
                {
                    Actors = new Data.Actor[] { gongyoo },
                    Name = "Guardian: The Lonely and Great God",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2016, 12, 2),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/68/Goblin_Poster.jpg",
                    Coordinator = "coord@example.com"
                });

                ds.Shows.Add(new Data.Show
                {
                    Actors = new Data.Actor[] { gongyoo },
                    Name = "Coffee Prince",
                    Genre = "Romance",
                    ReleaseDate = new DateTime(2007, 8, 28),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/99/Coffee_Prince.jpg",
                    Coordinator = "coord@example.com"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }
        public bool LoadEpisodes()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Episodes ***
            if (ds.Episodes.Count() == 0)
            {
                var guardian = ds.Shows.SingleOrDefault(s => s.Name == "Guardian: The Lonely and Great God");
                var coffee = ds.Shows.SingleOrDefault(s => s.Name == "Coffee Prince");

                ds.Episodes.Add(new Data.Episode
                {
                    Show = guardian,
                    Name = "Chapter 1",
                    SeasonNumber = 1,
                    EpisodeNumber = 1,
                    Genre = "Fantasy",
                    AirDate = new DateTime(2016, 12, 2),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/68/Goblin_Poster.jpg",
                    Clerk = "clerk@example.com"
                });
                ds.Episodes.Add(new Data.Episode
                {
                    Show = guardian,
                    Name = "Chapter 2",
                    SeasonNumber = 1,
                    EpisodeNumber = 2,
                    Genre = "Fantasy",
                    AirDate = new DateTime(2016, 12, 3),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/68/Goblin_Poster.jpg",
                    Clerk = "clerk@example.com"
                });
                ds.Episodes.Add(new Data.Episode
                {
                    Show = guardian,
                    Name = "Chapter 3",
                    SeasonNumber = 1,
                    EpisodeNumber = 3,
                    Genre = "Fantasy",
                    AirDate = new DateTime(2016, 12, 9),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/68/Goblin_Poster.jpg",
                    Clerk = "clerk@example.com"
                });

                ds.Episodes.Add(new Data.Episode
                {
                    Show = coffee,
                    Name = "Chapter 1",
                    SeasonNumber = 1,
                    EpisodeNumber = 1,
                    Genre = "Romance",
                    AirDate = new DateTime(2007, 7, 2),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/99/Coffee_Prince.jpg",
                    Clerk = "clerk@example.com"
                });
                ds.Episodes.Add(new Data.Episode
                {
                    Show = coffee,
                    Name = "Chapter 2",
                    SeasonNumber = 1,
                    EpisodeNumber = 2,
                    Genre = "Romance",
                    AirDate = new DateTime(2007, 7, 3),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/99/Coffee_Prince.jpg",
                    Clerk = "clerk@example.com"
                });
                ds.Episodes.Add(new Data.Episode
                {
                    Show = coffee,
                    Name = "Chapter 3",
                    SeasonNumber = 1,
                    EpisodeNumber = 3,
                    Genre = "Romance",
                    AirDate = new DateTime(2007, 7, 9),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/99/Coffee_Prince.jpg",
                    Clerk = "clerk@example.com"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

    }

    #endregion

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}