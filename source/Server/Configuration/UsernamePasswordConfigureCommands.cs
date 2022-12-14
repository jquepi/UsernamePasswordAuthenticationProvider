using System;
using System.Collections.Generic;
using Octopus.Diagnostics;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Configuration;

namespace Octopus.Server.Extensibility.Authentication.UsernamePassword.Configuration
{
    class UsernamePasswordConfigureCommands : IContributeToConfigureCommand
    {
        readonly ISystemLog log;
        readonly Lazy<IUsernamePasswordConfigurationStore> configurationStore;

        public UsernamePasswordConfigureCommands(
            ISystemLog log,
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
    }
}