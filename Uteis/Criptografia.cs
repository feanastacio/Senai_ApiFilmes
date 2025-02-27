namespace api_filmes_senai.Uteis
{
    public static class Criptografia
    {
        public static string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
        public static bool CompararHash(string senhaInformada, string senhaBranco)
        {
            return BCrypt.Net.BCrypt.Verify(senhaInformada, senhaBranco);
        }
    }

}
