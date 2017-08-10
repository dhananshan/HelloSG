using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGBotService.Model.ExternalService
{

    public class WeatherResponse
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public DateTime update_timestamp { get; set; }
        public DateTime timestamp { get; set; }
        public Valid_Period valid_period { get; set; }
        public General general { get; set; }
        public Period[] periods { get; set; }
    }

    public class Valid_Period
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class General
    {
        public string forecast { get; set; }
        public Relative_Humidity relative_humidity { get; set; }
        public Temperature temperature { get; set; }
        public Wind wind { get; set; }
    }

    public class Relative_Humidity
    {
        public int low { get; set; }
        public int high { get; set; }
    }

    public class Temperature
    {
        public int low { get; set; }
        public int high { get; set; }
    }

    public class Wind
    {
        public Speed speed { get; set; }
        public string direction { get; set; }
    }

    public class Speed
    {
        public int low { get; set; }
        public int high { get; set; }
    }

    public class Period
    {
        public Time time { get; set; }
        public Regions regions { get; set; }
    }

    public class Time
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class Regions
    {
        public string west { get; set; }
        public string east { get; set; }
        public string central { get; set; }
        public string south { get; set; }
        public string north { get; set; }
    }

}
