using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiDlyaKursovoyTry2.Models;

public partial class NumbersLuxeAndPoluLuxe
{
    public int IdNumbersLuxeAndPoluLuxe { get; set; }

    public int NumberOfRoom { get; set; }

    public string? TypeOfNumber { get; set; }

    public string? FreeOrClose { get; set; }

    public int CountOfRooms { get; set; }

    public int Floor { get; set; }

    public string? Phone { get; set; }

    public decimal CostPerDay { get; set; }

    public string? InfoAboutBron { get; set; }

    public int CountOfPeoples { get; set; }

    public int IdClient { get; set; }
    [JsonIgnore]
    public virtual Client IdClientNavigation { get; set; } = null!;
}
