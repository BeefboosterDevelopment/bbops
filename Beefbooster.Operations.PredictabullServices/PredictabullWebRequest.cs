using System;
using System.IO;
using System.Net;
using System.Text;

namespace Beefbooster.Operations.PredictabullServices
{
    public class PredictabullWebRequest
    {
        public string IssueWebRequest(string url, string json)
        {
            try
            {
                var httpReq = (HttpWebRequest) WebRequest.Create(url);

                if (!string.IsNullOrEmpty(json))
                {
                    httpReq.Method = "POST";
                    httpReq.ContentType = "application/json";
                    var encoding = new UTF8Encoding();
                    byte[] postData = encoding.GetBytes(json);
                    httpReq.ContentLength = postData.Length;

                    // convert the request to a stream object and send it on its way
                    Stream ReqStrm = httpReq.GetRequestStream();

                    ReqStrm.Write(postData, 0, postData.Length);
                    ReqStrm.Close();
                }
                else
                {
                    httpReq.Method = "GET";
                    httpReq.ContentLength = 0;
                }

                // get the response from the web server and read it all back into a string variable
                var httpResp = (HttpWebResponse) httpReq.GetResponse();
                var respStrm = new StreamReader(stream: httpResp.GetResponseStream(), encoding: Encoding.UTF8);

                string result = respStrm.ReadToEnd();

                httpResp.Close();
                respStrm.Close();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(
                    string.Format("Predictabull web service at {0} threw an exception. POST data:{1}", url, json), e);
            }
        }
    }
}