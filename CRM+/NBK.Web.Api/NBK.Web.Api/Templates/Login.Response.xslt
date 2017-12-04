<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
  xmlns:sec="http://NBK.com/Security"
  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"
  >

  <xsl:output method="text"/>
  <xsl:template match="/">
    <xsl:value-of select="soap:Envelope/soap:Body/sec:LoginResponse/sec:LoginResult/."/>
  </xsl:template>
</xsl:stylesheet>