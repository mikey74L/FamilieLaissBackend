using Breeze.ContextProvider;
using Breeze.WebApi2;
using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    [BreezeController]
    public class FamilieLaissController : ApiController
    {
        #region Private Members
        readonly iUnitOfWorkData _unitOfWork;
        #endregion

        #region C'tor
        public FamilieLaissController(iUnitOfWorkData unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Get Metadata
        [HttpGet]
        public string Metadata()
        {
            return _unitOfWork.Metadata();
        }
        #endregion

        #region Facet_Groups
        [HttpGet]
        public IQueryable<FacetGroup> FacetGroups()
        {
            return _unitOfWork.FacetGroupRepository.All();
        }
        #endregion

        #region Facet_Values
        [HttpGet]
        public IQueryable<FacetValue> FacetValues()
        {
            return _unitOfWork.FacetValueRepository.All();
        }
        #endregion

        #region Media_Groups
        [HttpGet]
        public IQueryable<MediaGroup> MediaGroups()
        {
            return _unitOfWork.MediaGroupRepository.All();
        }
        #endregion

        #region Media_Items
        [HttpGet]
        public IQueryable<MediaItem> MediaItems()
        {
            return _unitOfWork.MediaItemRepository.All();
        }

        [HttpGet]
        public IQueryable<MediaItemFacet> MediaItemFacets()
        {
            return _unitOfWork.MediaItemFacetRepository.All();
        }
        #endregion

        #region Picture-Upload
        [HttpGet]
        public IQueryable<UploadPictureItem> UploadPictures()
        {
            return _unitOfWork.UploadPictureItemRepository.All();
        }

        [HttpGet]
        public IQueryable<UploadPictureImageProperty> UploadPictureProperties()
        {
            return _unitOfWork.UploadPictureImagePropertyRepository.All();
        }
        #endregion

        #region Video-Upload
        [HttpGet]
        public IQueryable<UploadVideoItem> UploadVideos()
        {
            return _unitOfWork.UploadVideoItemRepository.All();
        }
        #endregion

        //#region Video-Converter Status
        //[HttpGet]
        //public IQueryable<Video_Convert_Status> Video_Convert_Stati()
        //{
        //    return _unitOfWork.VideoConvertStatusRepository.All();
        //}
        //#endregion

        //#region Message
        //[HttpGet]
        //public IQueryable<Message> Messages_Prio_1()
        //{
        //    return _unitOfWork.MessageRepository.All().Where(x => x.Prio == 1);
        //}

        //[HttpGet]
        //public IQueryable<Message> Messages_Prio_2()
        //{
        //    return _unitOfWork.MessageRepository.All().Where(x => x.Prio == 2);
        //}

        //[HttpGet]
        //public IQueryable<Message> Messages_Prio_3()
        //{
        //    return _unitOfWork.MessageRepository.All().Where(x => x.Prio == 3);
        //}
        //#endregion

        #region Save Changes
        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            //Speichern der Änderungen in der Datenbank
            return _unitOfWork.Commit(saveBundle);
        }
        #endregion
    }
}