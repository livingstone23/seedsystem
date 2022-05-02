﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.Viewmodel
{
    public record EmbeddedReportViewModel
    (
     string Id,
     string Name,
     string EmbedUrl,
     string Token
    );
}
