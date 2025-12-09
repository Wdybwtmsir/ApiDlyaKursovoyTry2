using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiDlyaKursovoyTry2.Models;

public partial class RegistrationCard
{
    public int IdRegistrationCards { get; set; }

    public int NumberOfClientRegistration { get; set; }

    public int NumberOfRoom { get; set; }

    public DateOnly? DataPribitiya { get; set; }

    public string? TypeOfDocument { get; set; }

    public int SerialAndNumberOfDocument { get; set; }

    public DateOnly? BirthDay { get; set; }

    public string? Sex { get; set; }

    public string? HomeAddress { get; set; }

    public int? IdClient { get; set; }

    public DateOnly? DataUbitiya { get; set; }
    [JsonIgnore]
    public virtual Client? IdClientNavigation { get; set; }
}
