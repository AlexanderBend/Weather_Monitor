using System;
using System.Collections.Generic;

namespace Weather_Monitor
{
    public class WeaterFacade
    {
        public Request Request;
        public Location Location;
        public Current Current;
    }

    public class Current
    {
        public string observation_time { get; set; }
        public long temperature { get; set; }
        public long weather_code { get; set; }
        public List<Uri> weather_icons { get; set; }
        public List<string> weather_descriptions { get; set; }
        public long wind_speed { get; set; }
        public long wind_degree { get; set; }
        public string wind_dir { get; set; }
        public long pressure { get; set; }
        public double precip { get; set; }
        public long humidity { get; set; }
        public long cloudcover { get; set; }
        public long feelslike { get; set; }
        public long uv_index { get; set; }
        public long visibility { get; set; }
        public string is_day { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string timezone_id { get; set; }
        public string localtime { get; set; }
        public long localtime_epoch { get; set; }
        public string utc_offset { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public string query { get; set; }
        public string language { get; set; }
        public string unit { get; set; }
    }
}
