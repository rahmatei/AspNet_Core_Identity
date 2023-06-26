using AAASamples.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AAASamples.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult List()
        {
            var roles= _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole(model.Name);
                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("list");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Name)
        {
            var identityRole=await _roleManager.FindByNameAsync(Name);
            if (identityRole != null)
            {
                var result = await _roleManager.DeleteAsync(identityRole);
                //if (!result.Succeeded)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        ModelState.AddModelError("", item.Description);
                //    }
                //}
            }
            return RedirectToAction("list");
        }


        public async Task<IActionResult> Edit (string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var RoleEdit = new EditRoleViewModel() { Id = role.Id, Name = role.Name };
            return View(RoleEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.FindByIdAsync(model.Id);
                if (result != null)
                {
                    result.Name = model.Name;
                    var ResultUpdate = await _roleManager.UpdateAsync(result);
                    if (ResultUpdate.Succeeded)
                    {
                        return RedirectToAction("list");
                    }
                    foreach (var item in ResultUpdate.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return RedirectToAction("list");
        }

    }
}
