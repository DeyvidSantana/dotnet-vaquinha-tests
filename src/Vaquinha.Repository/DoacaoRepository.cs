﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Repository.Context;

namespace Vaquinha.Repository
{
    public class DoacaoRepository : IDoacaoRepository
    {
        private readonly ILogger<DoacaoRepository> _logger;
        private readonly GloballAppConfig _globalSettings;
        private readonly IConfiguration _configuration;

        private readonly VaquinhaOnlineDBContext _vaquinhaOnlineDBContext;

        public DoacaoRepository(GloballAppConfig globalSettings,
                                VaquinhaOnlineDBContext convideDBContext,
                                ILogger<DoacaoRepository> logger,
                                IConfiguration configuration)
        {
            _globalSettings = globalSettings;
            _vaquinhaOnlineDBContext = convideDBContext;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task AdicionarAsync(Doacao model)
        {
            await _vaquinhaOnlineDBContext.Doacoes.AddAsync(model);
            await _vaquinhaOnlineDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doacao>> RecuperarDoadoesAsync(int pageIndex = 0)
        {
           return  await _vaquinhaOnlineDBContext
                .Doacoes
                //.Include("Pessoa")
                .Skip(pageIndex).Take(_globalSettings.QuantidadeRegistrosPaginacao).ToListAsync();           
        }
    }
}