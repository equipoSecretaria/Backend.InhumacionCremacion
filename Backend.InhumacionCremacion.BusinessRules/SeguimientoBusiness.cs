﻿using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.BusinessRules
{
    public class SeguimientoBusiness : Entities.Interface.Business.ISeguimientoBusiness
    {

        #region Attributes

        /// <summary>
        /// The telemetry exception
        /// </summary>
        private readonly ITelemetryException _telemetryException;

        /// <summary>
        /// _repositorySeguimiento
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Seguimiento> _repositorySeguimiento;

        /// <summary>
        /// _repositoryDominio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> _repositoryDominio;

        #endregion


        #region Constructor

        /// <summary>
        /// SeguimientoBusiness
        /// </summary>
        /// <param name="telemetryException"></param>
        /// <param name="repositorySeguimiento"></param>
        /// <param name="repositoryDominio"></param>
        public SeguimientoBusiness(ITelemetryException telemetryException,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Seguimiento> repositorySeguimiento,
                                   Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> repositoryDominio)
        {
            _telemetryException = telemetryException;
            _repositorySeguimiento = repositorySeguimiento;
            _repositoryDominio = repositoryDominio;
        }

        #endregion

        #region Methods
        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="seguimiento"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> AddSeguimiento(Seguimiento seguimiento)
        {
            try
            {
                if (seguimiento == null) { return new ResponseBase<bool>(code: HttpStatusCode.OK, message: "Los Campos son obligatorios", data: false); }
                await _repositorySeguimiento.AddAsync(new Seguimiento
                {
                    Id = Guid.NewGuid(),
                    Estado = seguimiento.Estado,
                    FechaRegistro = DateTime.Now,
                    Usuario = seguimiento.Usuario,
                    Observacion = seguimiento.Observacion,
                    IdSolicitud = seguimiento.IdSolicitud
                });

                return new ResponseBase<bool>(code: HttpStatusCode.OK, message: "Solicitud OK", data: true);
            }
            catch (System.Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<bool>(code: HttpStatusCode.InternalServerError, message: ex.Message, data: false);
            }
        }

        /// <summary>
        /// GetSeguimientoBySolicitud
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<Entities.DTOs.SeguimientoDto>>> GetSeguimientoBySolicitud(string idSolicitud)
        {
            try
            {

                var estadosTramites = await _repositoryDominio.GetAllAsync(w => w.TipoDominio.Equals(Guid.Parse("C5D41A74-09B6-4A7C-A45D-42792FCB4AC2")));

                var resultSolicitud = await _repositorySeguimiento.GetAllAsync(x => x.IdSolicitud.Equals(Guid.Parse(idSolicitud)));

                var resultJoin = (from rs in resultSolicitud
                                  join et in estadosTramites on rs.Estado equals et.Id
                                  select new Entities.DTOs.SeguimientoDto
                                  {
                                      Id = rs.Id,
                                      FechaRegistro = rs.FechaRegistro,
                                      IdSolicitud = rs.IdSolicitud,
                                      Observacion = rs.Observacion,
                                      Usuario = rs.Usuario.ToString(),
                                      Estado = et.Descripcion,
                                  }).ToList();

                if (resultJoin.Count() < 0)
                {
                    return new ResponseBase<List<Entities.DTOs.SeguimientoDto>>(HttpStatusCode.OK, message: "No se econtraron resultados");
                }
                return new ResponseBase<List<Entities.DTOs.SeguimientoDto>>(HttpStatusCode.OK, message: "Solicitud OK", data: resultJoin, count: resultJoin.Count());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<Entities.DTOs.SeguimientoDto>>(code: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }
        #endregion
    }
}
