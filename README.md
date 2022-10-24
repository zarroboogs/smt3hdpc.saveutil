
# SMT3HD Remaster PC Save Utility

A simple save decryption and encryption utility for SMT3HD PC.

## Requirements

- .NET Runtime 5.0+
- SMT3HD PC :)

## Usage

- Locate `SMT3HDDATA.sav` under `%APPDATA%/Roaming/SEGA/Steam/smt3hd/<account_id>/`

- To decrypt or encrypt the save:

  `smt3hdpc.saveutil.exe -i /path/to/SMT3HDDATA.sav`

  By default, the program outputs decrypted or encrypted save files with a `_dec` or `_enc` suffix respectively.

- To output to a specific path:

  `smt3hdpc.saveutil.exe -i /path/to/SMT3HDDATA.sav -o /path/to/output.sav`
