using System.ComponentModel.DataAnnotations;

namespace DCCRailway.Layout.Commands.Types;

/// <summary>
///     Allows you to Turn the FAST Clock on or Off and Set the Fast Clock
/// </summary>
public interface ICmdClockSet : ICommand {
    /// <summary>
    ///     Sets the Hour for the Clock in either 24 or 12 hour mode
    /// </summary>
    [Range(1, 24)]
    public int Hour { get; set; }

    /// <summary>
    ///     Set the minutes of the clock
    /// </summary>

    [Range(1, 60)]
    public int Minute { get; set; }

    /// <summary>
    ///     True if this is a 24 hour clock, false if it is a 12 hour clock
    /// </summary>
    public bool Is24Hour { set; }

    /// <summary>
    ///     Define the ratio (speed of the clock) in the range 1..15 (1:1 ... 1:15)
    /// </summary>
    [Range(1, 15)]
    public int Ratio { get; set; }
}