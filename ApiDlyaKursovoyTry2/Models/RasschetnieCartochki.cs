using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiDlyaKursovoyTry2.Models;

public partial class RasschetnieCartochki
{
    public int IdRegistrationCards { get; set; }

    public int NumberOfClientRegistration { get; set; }

    public int NumberOfRoom { get; set; }

    public DateOnly DataPribitiya { get; set; }

    public int IdClient { get; set; }

    public string? OplataBroni { get; set; }

    public DateOnly PredpologaemayaDataViezda { get; set; }

    public int CountOfOplachenieDni { get; set; }

    public int SumOfOplata { get; set; }

    public int OkonchatelniiRasschet { get; set; }
    [JsonIgnore]
    public virtual Client IdClientNavigation { get; set; } = null!;
}
