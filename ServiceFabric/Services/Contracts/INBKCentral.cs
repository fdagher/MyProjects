using NBK.Common.Foundations.Utilities;
using System.Data;
using System.ServiceModel;
using WCFExtrasPlus.Soap;

namespace Common
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [SoapHeaders]
    [ServiceContract]
    public interface INBKCentral
    {
        [OperationContract]
        DataSet Create(string targetCategory, DataSet inputData);

        [SoapHeader("Header", typeof(WebServiceHeader), Direction = SoapHeaderDirection.In)]
        [OperationContract]
        DataSet Read(string targetCategory, string viewName, FilterCriteria[] filterCriteria, SortCriteria[] sortCriteria, int startIndex, int numberOfRecords, ref int totalNumberOfRecords);

        [OperationContract]
        DataSet Update(string targetCategory, DataSet inputData);

        [OperationContract]
        int Delete(string targetCategory, FilterCriteria[] filterCriteria);

        [OperationContract]
        DataSet InvokeMethod(string targetCategory, string actionType, DataSet inputData);
    }
}
