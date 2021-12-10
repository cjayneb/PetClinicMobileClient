using System;
using System.Collections.Generic;
using System.Text;

namespace XPetClinic_MobileFrontend.Services.Network
{
    public static class ApiConstants
    {
        public static string GetOwnersURI()
        {
            return "http://10.0.2.2:80/api/gateway/owners";
        }

        public static string GetOwnerURI(int clientId)
        {
            return $"http://10.0.2.2:80/api/gateway/owners/{clientId}";
        }

        public static string GetLoginURI()
        {
            return "http://10.0.2.2:80/login";
        }

        public static string GetVetsURI()
        {
            return "http://10.0.2.2:80/api/gateway/vets";
        }

        public static string GetVisitsForPetURI(int petId)
        {
            return $"http://10.0.2.2:80/api/gateway/visits/pet/{petId}";
        }

        public static string GetVisitURI(string visitId)
        {
            return $"http://10.0.2.2:80/api/gateway/visits/{visitId}";
        }

        public static string GetVisitPostURI(int petId)
        {
            
            return $"http://10.0.2.2:80/api/gateway/owners/pets/{petId}/visits";
        }

        public static string GetVisitPutURI(string visitId, int petId)
        {
            return $"http://10.0.2.2:80/api/gateway/visits/{visitId}/owners/pets/{petId}";
        }

        public static string GetDeleteVisitURI(string visitId)
        {
            return $"http://10.0.2.2:80/api/gateway/visits/pet/{visitId}";
        }

    }
}
