using API_Zamdau.User;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Zamdau.DataBase
{
    public class StorageFirebase : BaseFirebase
    {
        string _pathStorage_Products = "Products";
        string _pathStorage_Sellers = "Sellers";

        public async Task<string?> UploadImage(string TokenID, Path path, IFormFile? file, string userID)
        {
            try
            {
                FirebaseStorageTask storage;
                switch (path)
                {
                    case Path.Products:
                        storage = new FirebaseStorage("projetoport-50b66.appspot.com",
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(TokenID),
                            ThrowOnCancel = true
                        }).Child(_pathStorage_Products)
                          .Child(userID)
                          .Child(Guid.NewGuid().ToString())
                          .PutAsync(file.OpenReadStream(), new CancellationTokenSource().Token);
                        return $"{await storage}";
                    case Path.Sellers:
                        storage = new FirebaseStorage("projetoport-50b66.appspot.com",
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(TokenID),
                            ThrowOnCancel = true
                        }).Child(_pathStorage_Sellers)
                          .Child(userID)
                          .Child(Guid.NewGuid().ToString())
                          .PutAsync(file.OpenReadStream(), new CancellationTokenSource().Token);
                        return $"{await storage}";
                    default:
                        return "Out of Range";
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task DeleteImage(Array produtos, string tokenID, string userID)
        {
            try
            {
                foreach (var NomeDaFoto in produtos)
                {

                    var storage = new FirebaseStorage("projetoport-50b66.appspot.com",
                              new FirebaseStorageOptions
                              {
                                  AuthTokenAsyncFactory = () => Task.FromResult(tokenID),
                                  ThrowOnCancel = true
                              }).Child(_pathStorage_Products).Child(userID).Child(NomeDaFoto.ToString()).DeleteAsync();

                    storage.Wait();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteOneImageAsync(string tokenID, string userID, Path path, string url)
        {

            try
            {
                Task? storage;
                switch (path)
                {
                    case Path.Products:
                        storage = new FirebaseStorage("projetoport-50b66.appspot.com",
                         new FirebaseStorageOptions
                         {
                             AuthTokenAsyncFactory = () => Task.FromResult(tokenID),
                             ThrowOnCancel = true
                         }).Child(_pathStorage_Products).Child(userID).Child(GetNameImgUrl(url)).DeleteAsync();
                        storage.Wait();
                        break;
                    case Path.Sellers:
                        storage = new FirebaseStorage("projetoport-50b66.appspot.com",
                         new FirebaseStorageOptions
                         {
                             AuthTokenAsyncFactory = () => Task.FromResult(tokenID),
                             ThrowOnCancel = true
                         }).Child(_pathStorage_Sellers).Child(userID).Child(GetNameImgUrl(url)).DeleteAsync();
                        storage.Wait();
                        break;
                    default:
                        break;
                }


            }
            catch (Exception)
            {
                throw;
            }


        }
        private string GetNameImgUrl(string url)
        {
            var index = url.IndexOf("?alt");
            var result = url.Substring(index - 36, 36);
            return result;
        }
    }
}
