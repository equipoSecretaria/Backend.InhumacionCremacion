using System;

namespace Backend.InhumacionCremacion.Utilities.Telemetry
{
    public interface ITelemetryException
    {
        void RegisterException(Exception exception);
    }
}