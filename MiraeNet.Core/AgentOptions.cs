namespace MiraeNet.Core;

/// <summary>
///     A set of properties used to configure the core services.
/// </summary>
public class AgentOptions
{
    /// <summary>
    ///     The system message used to configure the agent's persona.
    /// </summary>
    public string? SystemMessage { get; set; }
}