using Services.DomainModel;
using Services__ArqBase_.Bll;
using System.Configuration;

string securityConString = ConfigurationManager.ConnectionStrings["SecurityString"].ConnectionString;
Dal.Tools.SqlHelper.Initialize(securityConString);
PermisosGenericService p = new PermisosGenericService();
//1E7116B3-B6AC-439A-8A33-110F68F75E93 perm
//0A7C5D0D-7D9D-4FCD-B8B3-7B80F8306181 perm
//3075A247-1996-47F6-9ADC-B52ADB6E501E perm
//6F5E615B-70C2-4B05-AAF7-C12F88D3645A rol

List<Patente> patentes = new List<Patente>();

patentes.Add(new Patente(Guid.Parse("1E7116B3-B6AC-439A-8A33-110F68F75E93")));
patentes.Add(new Patente(Guid.Parse("0A7C5D0D-7D9D-4FCD-B8B3-7B80F8306181")));
patentes.Add(new Patente(Guid.Parse("3075A247-1996-47F6-9ADC-B52ADB6E501E")));

p.Asignar<Familia, Patente>(new Familia(Guid.Parse("6F5E615B-70C2-4B05-AAF7-C12F88D3645A")), patentes);