﻿using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Vostok.Airlock;
using Vostok.Logging.Serilog.Enrichers;
using Vostok.Logging.Serilog.Sinks;

namespace Vostok.Logging.Serilog
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration Airlock(
            this LoggerSinkConfiguration loggerConfiguration,
            IAirlock airlock,
            string routingKey,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(nameof(loggerConfiguration));

            var sink = new AirlockSink(airlock, routingKey);
            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        public static LoggerConfiguration WithHost(
            this LoggerEnrichmentConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(nameof(loggerConfiguration));

            var enricher = new HostEnricher();
            return loggerConfiguration.With(enricher);
        }
    }
}