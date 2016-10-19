﻿using System;
using System.Collections.Generic;
using Octopus.Diagnostics;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Configuration;

namespace Octopus.Server.Extensibility.Authentication.UsernamePassword.Configuration
{
    public class UsernamePasswordConfigureCommands : IContributeToConfigureCommand, IHandleLegacyWebAuthenticationModeConfigurationCommand
    {
        readonly ILog log;
        readonly Lazy<IUsernamePasswordConfigurationStore> configurationStore;

        public UsernamePasswordConfigureCommands(
            ILog log,
            Lazy<IUsernamePasswordConfigurationStore> configurationStore)
        {
            this.log = log;
            this.configurationStore = configurationStore;
        }

        public IEnumerable<ConfigureCommandOption> GetOptions()
        {
            yield return new ConfigureCommandOption("usernamePasswordIsEnabled=", "Set whether Octopus username/password authentication is enabled.", v =>
            {
                var isEnabled = bool.Parse(v);
                configurationStore.Value.SetIsEnabled(isEnabled);
                log.Info($"Octopus username/password authentication IsEnabled set to: {isEnabled}");
            });
        }

        public void Handle(string webAuthenticationMode)
        {
            var isEnabled = webAuthenticationMode == "UsernamePassword";
            configurationStore.Value.SetIsEnabled(isEnabled);
            log.Info($"Octopus username/password authentication IsEnabled set, based on webAuthenticationMode={webAuthenticationMode}, to: {isEnabled}");
        }
    }
}