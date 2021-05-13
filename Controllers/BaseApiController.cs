using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Data.Entities;
using UniWall.Models.Responses;

namespace UniWall.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IWebHostEnvironment _env;
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly ApiDbContext _db;

        private const int PAGE_SIZE = 3; 

        public BaseApiController(ApiDbContext db, IMapper mapper, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _env = env;
            _userManager = userManager;
        }

        protected T Map<T>(object source)
        {
            return _mapper.Map<T>(source);
        }

        protected T[] Map<T>(object[] source)
        {
            T[] results = new T[source.Length];

            for(int i = 0; i < source.Length; i++)
            {
                results[i] = Map<T>(source[i]);
            }

            return results;
        }

        protected IdentityUser GetUser()
        {
            IdentityUser user = null;
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userName != null)
            {
                Task<IdentityUser> finder = _userManager.FindByNameAsync(userName);
                finder.Wait();
                user = finder.Result;
            }

            return user;
        }

        protected IList<string> GetUserRoles()
        {
            IList<string> roles = new List<string>();

            IdentityUser user = GetUser();
            if(user != null)
            {
                Task<IList<string>> finder = _userManager.GetRolesAsync(user);
                finder.Wait();
                roles = finder.Result;
            }

            return roles;
        }

        protected PaginatedListResponse<V> Paginate<T, V>(IQueryable<T> query, int? pageNumber = null)
        {
            var list = query.ToPagedList(pageNumber ?? 1, PAGE_SIZE);

            return new()
            {
                CurrentPage = list.PageNumber,
                PageSize = list.PageSize,
                PagesTotal = list.PageCount,
                Data = Map<V[]>(list.ToArray())
            };
        }

        protected ICollection<T> UnifyCollections<T>(ICollection<T> current, IEnumerable<int> requestedIds)
            where T : IEntity
        {
            IEnumerable<int> currentIds = current.Select(i => i.Id);
            IEnumerable<T> toRemove = FindItemsToRemove(current, currentIds.Except(requestedIds));
            IEnumerable<T> toAdd = FindItemsToAdd<T>(requestedIds.Except(currentIds));
            
            foreach(T item in toRemove)
            {
                current.Remove(item);
            }

            foreach (T item in toAdd)
            {
                current.Add(item);
            }

            return current;
        }

        private IEnumerable<T> FindItemsToRemove<T>(ICollection<T> container, IEnumerable<int> requestedIds)
            where T : IEntity
        {
            return  container.Where(i => requestedIds.Contains(i.Id)).ToList();
        }

        private IEnumerable<T> FindItemsToAdd<T>(IEnumerable<int> requestedIds)
            where T : IEntity
        {
            List<T> toAdd = new();
            foreach(int id in requestedIds)
            {
                T obj = (T) _db.Find(typeof(T), id);
                if(obj != null)
                {
                    toAdd.Add(obj);
                }
            }
            return toAdd;
        }

    }
}
