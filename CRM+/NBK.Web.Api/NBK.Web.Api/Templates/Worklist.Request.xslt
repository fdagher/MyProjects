<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="token"/>
  <xsl:param name="partyID"/>
 
  <xsl:template match="/">
    <s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
      <s:Header>
        <SessionToken xmlns="http://nbk.com/ews/dataservice/security">
          <xsl:value-of select="$token"/>
        </SessionToken>
      </s:Header>
      <s:Body>
        <Search xmlns="NBK.EWS.Controller.WCF.ActivityInstance" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:diffgr="urn:schemas-microsoft-com:xml-diffgram-v1" xmlns:ws="http://NBK.com/EWSTS" xmlns:b="http://NBK.com/Common" xmlns:nbk="http://NBK.com">
          <filter xmlns:a="http://nbk.com/ews/dataservice" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
            <a:FilterCriteria>
              <a:CompOp>Equal</a:CompOp>
              <a:CondOp>And</a:CondOp>
              <a:FieldName>Status</a:FieldName>
              <a:FieldValue xmlns:c="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
                <c:string>Active</c:string>
              </a:FieldValue>
              <a:SubExpr>None</a:SubExpr>
            </a:FilterCriteria>
            <a:FilterCriteria>
              <a:CompOp>Equal</a:CompOp>
              <a:CondOp>And</a:CondOp>
              <a:FieldName>AssignedTo</a:FieldName>
              <a:FieldValue xmlns:c="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
                <c:string><xsl:value-of select="$partyID"/></c:string>
              </a:FieldValue>
              <a:SubExpr>None</a:SubExpr>
            </a:FilterCriteria>
            <a:FilterCriteria>
              <a:CompOp>Equal</a:CompOp>
              <a:CondOp>And</a:CondOp>
              <a:FieldName>Category</a:FieldName>
              <a:FieldValue xmlns:c="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
                <c:string>CRM</c:string>
              </a:FieldValue>
              <a:SubExpr>None</a:SubExpr>
            </a:FilterCriteria>
            <a:FilterCriteria>
              <a:CompOp>Like</a:CompOp>
              <a:CondOp>And</a:CondOp>
              <a:FieldName>Description</a:FieldName>
              <a:FieldValue xmlns:c="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
                <c:string>%%</c:string>
              </a:FieldValue>
              <a:SubExpr>None</a:SubExpr>
            </a:FilterCriteria>
            <a:FilterCriteria>
              <a:CompOp>GreaterThanEqual</a:CompOp>
              <a:CondOp>And</a:CondOp>
              <a:FieldName>StartTime</a:FieldName>
              <a:FieldValue xmlns:c="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
                <c:string>2016-01-01T00:00:00</c:string>
              </a:FieldValue>
              <a:SubExpr>None</a:SubExpr>
            </a:FilterCriteria>
            <a:FilterCriteria>
              <a:CompOp>LessThanEqual</a:CompOp>
              <a:CondOp>And</a:CondOp>
              <a:FieldName>StartTime</a:FieldName>
              <a:FieldValue xmlns:c="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
                <c:string>2016-10-01T00:00:00</c:string>
              </a:FieldValue>
              <a:SubExpr>None</a:SubExpr>
            </a:FilterCriteria>
          </filter>
          <sort xmlns:a="http://nbk.com/ews/dataservice" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
            <a:SortCriteria>
              <a:FieldName>StartTime</a:FieldName>
              <a:IsDesc>true</a:IsDesc>
            </a:SortCriteria>
          </sort>
        </Search>
      </s:Body>
    </s:Envelope>

  </xsl:template>
</xsl:stylesheet>