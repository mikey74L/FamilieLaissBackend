using Breeze.ContextProvider;
using FamilieLaissBackend.Data.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.Interface
{
    public interface iUnitOfWorkData
    {
        //Das Repository für die Alben
        iRepositoryData<MediaGroup> MediaGroupRepository { get; }
        iRepositoryData<FacetGroup> FacetGroupRepository { get; }
        iRepositoryData<FacetValue> FacetValueRepository { get; }
        iRepositoryData<MediaItem> MediaItemRepository { get; }
        iRepositoryData<MediaItemFacet> MediaItemFacetRepository { get; }
        iRepositoryData<UploadPictureItem> UploadPictureItemRepository { get; }
        iRepositoryData<UploadPictureImageProperty> UploadPictureImagePropertyRepository { get; }
        iRepositoryData<UploadVideoItem> UploadVideoItemRepository { get; }
        //iDataRepository<Video_Convert_Status> VideoConvertStatusRepository { get; }
        //iDataRepository<Message> MessageRepository { get; }

        //Metadaten für Breeze ermitteln
        string Metadata();

        //Die Änderungen an den Entities mit Breeze speichern
        SaveResult Commit(JObject changeSet);
    }
}
