using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiDlyaKursovoyTry2.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string? FirstName { get; set; }

    public string? SurName { get; set; }

    public string? LastName { get; set; }

    public string? TypeOfDocument { get; set; }

    public int SerialAndNumberOfDocument { get; set; }

    public DateOnly? BirthDay { get; set; }

    public string? Sex { get; set; }

    public string? HomeAddress { get; set; }

    public int NumberOfClientRegistration { get; set; }

    public int NumberOfRoom { get; set; }

    public string? Phone { get; set; }
    [JsonIgnore]
    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();
    [JsonIgnore]
    public virtual ICollection<NumbersLuxeAndPoluLuxe> NumbersLuxeAndPoluLuxes { get; set; } = new List<NumbersLuxeAndPoluLuxe>();
    [JsonIgnore]
    public virtual ICollection<NumbersOther> NumbersOthers { get; set; } = new List<NumbersOther>();
    [JsonIgnore]
    public virtual ICollection<RasschetnieCartochki> RasschetnieCartochkis { get; set; } = new List<RasschetnieCartochki>();
    [JsonIgnore]
    public virtual ICollection<RegistrationCard> RegistrationCards { get; set; } = new List<RegistrationCard>();
}
