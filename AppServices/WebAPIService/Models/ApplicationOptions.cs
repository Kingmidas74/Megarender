using System;

namespace Megarender.WebAPIService.Models {
    public class ApplicationOptions {
        public String IdentityServiceURI { get; set; }
        public String RabbitMQSeriveURI { get; set; }
    }
}