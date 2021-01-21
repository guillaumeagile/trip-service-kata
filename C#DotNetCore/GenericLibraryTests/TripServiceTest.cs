using Xunit;
using TripServiceKata.Trip;
using TripServiceKata.User;
//using Moq;

namespace TripServiceKata.Tests
{
    
    public class TripServiceTest
    {
        [Fact]
        public void HappyPath()
        {
          
            var user = new User.User();
            //IUserSession iUserSession = UserSession.GetInstance();  // BAD DESIGN: ceci est un Singleton 
            // le problème c'est qu'on ne peut pas influencer ce que fait le UserSession, 
            // on est fortement couplé à l'implementation UserSession, et tout ce qui peut se passer de chelou (se prendre des exceptions dans la face, par exemple)
            
            // pour éviter cela, il suffit de remplacer (Liskov Substitution) l'implémentation par une abstraction
            // de choisir ensuite l'implémentation adaptée au test (une doublure ou un mock)
            IUserSession iUserSession = new UserSessionForTest();  // ON PEUT AUSSI repmplacer cette doublure par un Mock

            //et on fait l'injection de cette dépendance , toujours typée sur l'Interface choisie  (IQuelqueChose)
            var sut = new TripService( iUserSession);

            // maintenant le reste du code se comporte tout aussi bien que si on avait utilisé le Singleton d'origine
            var res = sut.GetTripsByUser(user);
            Assert.NotNull(res);
            Assert.NotEmpty(res);
        }
    }
}
