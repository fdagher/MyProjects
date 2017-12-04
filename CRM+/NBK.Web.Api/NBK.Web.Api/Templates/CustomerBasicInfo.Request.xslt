<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="token"/>
  <xsl:param name="customerNumber"/>

  <xsl:template match="/">
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:nbk="http://NBK.com/NBK.EAI.Services.Web/NBKCentral">
      <soapenv:Header>
        <nbk:WebServiceHeader>
          <nbk:Identifier>
            <xsl:value-of select="$token"/>
          </nbk:Identifier>
          <nbk:ITData/>
        </nbk:WebServiceHeader>
      </soapenv:Header>
      <soapenv:Body>
        <nbk:Read>
          <nbk:targetCategory>Customer</nbk:targetCategory>
          <nbk:viewName>CustomerBasicInfo</nbk:viewName>
          <nbk:filterCriteria>
            <nbk:FilterCriteria>
              <nbk:FieldName>CustomerNo</nbk:FieldName>
              <nbk:FieldValue>
                <nbk:string>
                  <xsl:value-of select="$customerNumber"/>
                </nbk:string>
              </nbk:FieldValue>
              <nbk:CompOp>Equal</nbk:CompOp>
              <nbk:CondOp>Or</nbk:CondOp>
              <nbk:SubExpr>None</nbk:SubExpr>
            </nbk:FilterCriteria>
          </nbk:filterCriteria>
          <nbk:startIndex>-1</nbk:startIndex>
          <nbk:numberOfRecords>-1</nbk:numberOfRecords>
          <nbk:totalNumberOfRecords>0</nbk:totalNumberOfRecords>
        </nbk:Read>
      </soapenv:Body>
    </soapenv:Envelope>

  </xsl:template>
</xsl:stylesheet>