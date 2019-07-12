// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace CleanBlog.Web.Routing
{
    public class CustomEndpointDataSource : EndpointDataSource
    {
        private List<CustomEndpointConventionBuilder> _endpointConventionBuilders;

        public CustomEndpointDataSource()
        {
            _endpointConventionBuilders = new List<CustomEndpointConventionBuilder>();
        }

        public IEndpointConventionBuilder AddEndpointBuilder(EndpointBuilder endpointBuilder)
        {
            var builder = new CustomEndpointConventionBuilder(endpointBuilder);
            _endpointConventionBuilders.Add(builder);

            return builder;
        }

        public override IChangeToken GetChangeToken()
        {
            return NullChangeToken.Singleton;
        }

        public override IReadOnlyList<Endpoint> Endpoints => _endpointConventionBuilders.Select(e => e.Build()).ToArray();

        // for testing
        internal IEnumerable<EndpointBuilder> EndpointBuilders => _endpointConventionBuilders.Select(b => b.EndpointBuilder);
    }
}
