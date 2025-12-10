using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KursClient.Models;

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

}
