﻿using MainTz.Web.ViewModels.CarModelViewModel;
using MainTz.Web.ViewModels.ImageViewModels;
using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public IEnumerable<ImageRequest> Images { get; set; }

        public IEnumerable<UserRequest> Users { get; set; }

		public int ModelId { get; set; }
        public ModelRequest Model { get; set; }
    }
}