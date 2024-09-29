using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using YJK2237A1.Data;
using YJK2237A1.Models;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-649bc213-79bf-48f1-9ad6-cbfb514066e2
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace YJK2237A1.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instances
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                cfg.CreateMap<VenueAddViewModel, Venue>()
                    .ForMember(dest => dest.VenueId, opt => opt.Ignore());
                cfg.CreateMap<Venue, VenueAddViewModel>();
                cfg.CreateMap<Venue, VenueBaseViewModel>();
                cfg.CreateMap<VenueBaseViewModel, Venue>();
                cfg.CreateMap<VenueBaseViewModel, VenueEditFormViewModel>()
                    .ForMember(dest => dest.TicketSalePassword, opt => opt.MapFrom(src => "pass"))
                    .ForMember(dest => dest.PromoCode, opt => opt.MapFrom(src => "promo"))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => 0));
                cfg.CreateMap<VenueEditFormViewModel, VenueEditViewModel>()
                    .ForMember(dest => dest.TicketSalePassword, opt => opt.Ignore())
                    .ForMember(dest => dest.PromoCode, opt => opt.Ignore())
                    .ForMember(dest => dest.Capacity, opt => opt.Ignore());
                cfg.CreateMap<VenueEditViewModel, VenueBaseViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;

            config.AssertConfigurationIsValid();

        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().
        
        public IEnumerable<VenueBaseViewModel> VenueGetAll()
        {
            var venues = ds.Venues.OrderBy(v => v.Company);
            return mapper.Map<IEnumerable<VenueBaseViewModel>>(venues);
        }

        public VenueBaseViewModel VenueGetById(int id)
        {
            var venue = ds.Venues.SingleOrDefault(v => v.VenueId == id);
            return venue == null ? null : mapper.Map<VenueBaseViewModel>(venue);
        }

        // 3. Add new
        public VenueBaseViewModel VenueAdd(VenueAddViewModel item)
        {
            var entity = mapper.Map<Venue>(item);
            ds.Venues.Add(entity);
            ds.SaveChanges();

            return mapper.Map<VenueBaseViewModel>(entity);
        }

        // 4. Edit existing
        public VenueBaseViewModel VenueEdit(VenueEditViewModel item)
        {
            var existingVenue = ds.Venues.SingleOrDefault(v => v.VenueId == item.VenueId);

            if (existingVenue == null)
            {
                return null;
            }
            else
            {
                var BaseViewModel = mapper.Map<VenueBaseViewModel>(existingVenue);
                mapper.Map(item, BaseViewModel);
                ds.SaveChanges();

                return BaseViewModel;
            }
        }

        // 5. Delete existing
        public bool VenueDelete(int id)
        {
            var existingVenue = ds.Venues.SingleOrDefault(v => v.VenueId == id);

            if (existingVenue == null)
            {
                return false;
            }

            ds.Venues.Remove(existingVenue);
            ds.SaveChanges();
            return true;
        }
    }
}