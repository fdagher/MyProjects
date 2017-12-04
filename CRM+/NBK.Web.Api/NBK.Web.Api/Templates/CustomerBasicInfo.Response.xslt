<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"
  xmlns:diffgr="urn:schemas-microsoft-com:xml-diffgram-v1"
  xmlns:resp ="http://NBK.com/Customer_Product_Account"
  xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"
  xmlns:ds ="http://NBK.com/Customer_Product_Account_SearchMiniStatement_Ds.xsd">

  <xsl:template match="/">
    <CustomerBasicInfoResponse>
   
      <FullNameEnglish>
        <xsl:value-of select="soap:Envelope/soap:Body/ReadResponse/ReadResult/diffgr:diffgram/ds:Customer_Product_Account_SearchMiniStatement_Ds/ds:OutputParam/ds:NO_OF_TRANS/."/>
      </FullNameEnglish>
   
    </CustomerBasicInfoResponse>
  </xsl:template>

</xsl:stylesheet>
