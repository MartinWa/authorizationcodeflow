using System.Collections.Generic;
using System.Threading.Tasks;
using authorizationcodeflow.Models;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace authorizationcodeflow.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private static Dictionary<string, AuthorizeState> _stateStore;

        public HomeController(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _stateStore = new Dictionary<string, AuthorizeState>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Discovery(DiscoveryModel model)
        {
            var discoveryClient = new DiscoveryClient(model.Url);
            var doc = await discoveryClient.GetAsync();
            if (doc.IsError)
            {
                return Error(doc.Error);
            }
            return View(new DiscoveryViewModel
            {
                DiscoveryResponse = doc,
                Issuer = doc.Issuer
            });
        }

        public async Task<IActionResult> Authorize(AuthorizeModel model)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;
            var redirectUri = urlHelper.Action("Callback", "Home", null, scheme).ToLower();
            var options = new OidcClientOptions
            {
                Authority = model.Issuer,
                ClientId = model.ClientId,
                RedirectUri = redirectUri,
                Scope = model.Scope,
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.FormPost
            };
            var client = new OidcClient(options);
            var state = await client.PrepareLoginAsync();
            _stateStore.Add(state.State, state);
            return Redirect(state.StartUrl);
        }

        public IActionResult Callback(CallbackModel model)
        {
            var result = await client.ProcessResponseAsync(data, state);
            var url = Request.QueryString;
            if (_stateStore.TryGetValue(model.State, out AuthorizeState result))
            {
                return View("Error", $"Session {model.State} was not found");
            }
            // Verify
            var code = model.Code;

        }

        public IActionResult Error(string error = "")
        {
            return View("Error", error);
        }
    }
}
