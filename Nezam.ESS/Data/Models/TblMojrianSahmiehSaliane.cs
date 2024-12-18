﻿namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianSahmiehSaliane
{
    public long Id { get; set; }

    public long? MojriId { get; set; }

    public int? Sal { get; set; }

    public double? TedadKar { get; set; }

    public double? Metraj { get; set; }

    public string? Comments { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblMojrianInfo? Mojri { get; set; }
}