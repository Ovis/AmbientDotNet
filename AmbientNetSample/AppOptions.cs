using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbientNetSample
{
    public class AppOptions
    {

        [Required]
        public string ChannelId { get; set; }

        [Required]
        public string WriteKey { get; set; }

        [Required]
        public string ReadKey { get; set; }
    }
}
