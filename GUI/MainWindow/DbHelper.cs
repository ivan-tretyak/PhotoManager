//PhotoManager. Program to organize your photo to album.
//Copyright (C) 2021 Ivan Tretyak Nickolaevich
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA

using System.Collections.Generic;
using System.Data;
using System.Linq;
using ORMDatabaseModule;


namespace PhotoManager
{
    public class DbHelper
    {
        public DbHelper()
        {

        }
        public List<Album> GetAlbums()
        {
            using var db = new DatabaseContext();
            return db.Albums.ToList();
        }
        public List<Photo> GetPhotos(List<AlbumContext> albumContexts)
        {
            using var db = new DatabaseContext();
            List<Photo> photos = new();
            foreach (var albumContext in albumContexts)
            {
                photos.Add(db.Photos
                    .Where(photo => photo.PhotoId == albumContext.PhotoId)
                    .First());
            }
            return photos;
        }
        public List<Photo> GetPhotos(List<AlbumContext> albumContexts, string year)
        {
            year = SmallUtils.NormalizeYear(year);
            using var db = new DatabaseContext();
                //Select photo
                List<Photo> photos = new();
            foreach (var albumContext in albumContexts)
            {
                Photo photo = db.Photos
                    .Where(photo => photo.PhotoId == albumContext.PhotoId && photo.MetaData.DateCreation.Contains(year))
                    .FirstOrDefault();
                if (photo is not null)
                    photos.Add(photo);
            }
            return photos;
        }
        public List<AlbumContext> GetAlbumContexts(string AlbumName)
        {
            using var db = new DatabaseContext();
            return db.AlbumContexts
                        .Where(albumContext => albumContext.Album.Name == AlbumName)
                        .ToList();
        }
        public List<MetaData> GetMetaDatas(string AlbumName)
        {
            //Get album context
            var albumContexts = GetAlbumContexts(AlbumName);

            //Get photo
            var photos = GetPhotos(albumContexts);

            //Select metadata
            using var db = new DatabaseContext();
            List<MetaData> metadatas = new();
            foreach (var photo in photos)
            {
                metadatas.Add(db.MetaDatas
                    .Where(metadata => metadata.MetadataId == photo.MetaDataId)
                    .First());
            }
            return metadatas;
        }

        public void MoveToAnotherAlbum(string newAlbumName, string oldAlbumName, string path)
        {
            using (var db = new DatabaseContext())
            {
                //Get album context
                var albumContext = db.AlbumContexts
                    .Where(aC => aC.Album.Name == oldAlbumName && aC.Photo.Path == path)
                    .First();

                //Get destation album
                var album = db.Albums
                    .Where(a => a.Name == newAlbumName)
                    .First();

                //Update
                albumContext.Album = album;
                albumContext.AlbumId = album.AlbumId;
                db.AlbumContexts.Update(albumContext);
                db.SaveChanges();
            }
        }

        public void CopyToAnotherAlbum(string newAlbumName, string path)
        {
            using (var db = new DatabaseContext())
            {
                //Create new album context
                var albumContext = new AlbumContext();

                //Get destation album
                var album = db.Albums
                    .Where(a => a.Name == newAlbumName)
                    .First();

                //Get photo by path
                var photo = db.Photos
                    .Where(p => p.Path == path)
                    .First();

                //Associate
                albumContext.Album = album;
                albumContext.Photo = photo;
                db.Add(albumContext);
                db.SaveChanges();
            }
        }
        public void UpdateExist(Photo photo, int existFlag)
        {
            using var db = new DatabaseContext();
            photo.Exist = existFlag;
            db.Photos.Update(photo);
            db.SaveChanges();
        }
        public void Remove(string album, string path)
        {
            using (var db = new DatabaseContext())
            {
                //Get album context
                var albumContext = db.AlbumContexts
                     .Where(aC => aC.Photo.Path == path && aC.Album.Name == album)
                     .First();

                db.AlbumContexts.Remove(albumContext);
                db.SaveChanges();
            }
        }
    }
}
