using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Token
{
    public class TokenJWTBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string,string> claims = new Dictionary<string,string>();
        private int expiryInMinutes = 5;

        public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public TokenJWTBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public TokenJWTBuilder AddClaims(Dictionary<string, string>claims)
        {
            this.claims.Union(claims);
            return this;
        }
        public TokenJWTBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }
        public TokenJWTBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }
        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null or empty.");

            
        }
        //public TokenJWT Build()
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, subject),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    foreach (var claim in claims)
        //    {
        //        tokenDescriptor.Subject.AddClaim(new Claim(claim.Key, claim.Value));
        //    }

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return new TokenJWT(token);
        //}
    }

    

}

