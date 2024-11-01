﻿using Newtonsoft.Json;

namespace PackingClassLibrary;

public class PackingConfiguration
{
    [JsonProperty("pallet_id", Required = Required.Default)]
    public string PalletId { get; set; }

    // pallet dimensions
    [JsonProperty("pallet_width", Required = Required.Default)]
    public int PalletWidth { get; set; } 

    [JsonProperty("pallet_height", Required = Required.Default)]
    public int PalletHeight { get; set; } 

    [JsonProperty("pallet_length", Required = Required.Default)]
    public int PalletLength { get; set; } 

    [JsonProperty("pallet_weight", Required = Required.Default)]
    public int PalletWeight { get; set; }

    // dimensional restrictions
    [JsonProperty("max_width", Required = Required.Default)]
    public int MaxWidth { get; set; }

    [JsonProperty("max_height", Required = Required.Default)]
    public int MaxHeight { get; set; }

    [JsonProperty("max_length", Required = Required.Default)]
    public int MaxLength { get; set; }

    [JsonProperty("max_weight", Required = Required.Default)]
    public int MaxWeight { get; set; }

    // packing alignment
    [JsonProperty("alignment_length", Required = Required.Default)]
    public PackingAlignment AlignmentLength { get; set; } // (0..centre; 1..left, 2..right

    [JsonProperty("alignment_width", Required = Required.Default)]
    public PackingAlignment AlignmentWidth { get; set; } // (0..centre; 1..left, 2..right
}