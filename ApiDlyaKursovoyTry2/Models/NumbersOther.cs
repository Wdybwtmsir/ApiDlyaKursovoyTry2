using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiDlyaKursovoyTry2.Models;

public partial class NumbersOther
{
    public int IdNumbersOther { get; set; }

    public int NumberOfRoom { get; set; }

    public string? TypeOfNumber { get; set; }

    public int CountOfMest { get; set; }

    public int Floor { get; set; }

    public string? Phone { get; set; }

    public decimal CostPerDay { get; set; }

    public int CountOfFreePlaces { get; set; }

    public int IdClient { get; set; }
    [JsonIgnore]
    public virtual Client IdClientNavigation { get; set; } = null!;
}
