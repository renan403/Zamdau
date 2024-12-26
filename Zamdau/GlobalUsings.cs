global using System;
global using System.Globalization;
global using System.Security.Claims;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
/* padrao br
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

 decimal valor = 1234.56m;
string valorFormatado = valor.ToString("C", CultureInfo.DefaultThreadCurrentUICulture);
Console.WriteLine(valorFormatado);  // Saída: R$ 1.234,56

 */

