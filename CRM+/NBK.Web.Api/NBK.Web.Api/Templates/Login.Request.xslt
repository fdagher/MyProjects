<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="systemstore"/>
  <xsl:param name="systemname"/>
  <xsl:param name="systempassword"/>
  <xsl:param name="userstore"/>
  <xsl:param name="username"/>
  <xsl:param name="userpassword"/>

  <xsl:template match="/">
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:wsa="http://schemas.xmlsoap.org/ws/2004/03/addressing" xmlns:wsse="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd" xmlns:wsu="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd">
      <soap:Header>
        <BrokerHeader xmlns="http://NBK.com/Common">
          <SystemCredentialStore>
            <xsl:value-of select="$systemstore"/>
          </SystemCredentialStore>
          <SystemId>
            <xsl:value-of select="$systemname"/>
          </SystemId>
          <SystemPassowrd>
            <xsl:value-of select="$systempassword"/>
          </SystemPassowrd>
          <UserCredentialStore>
            <xsl:value-of select="$userstore"/>
          </UserCredentialStore>
          <UserId>
            <xsl:value-of select="$username"/>
          </UserId>
          <UserPassword>
            <xsl:value-of select="$userpassword"/>
          </UserPassword>
          <ITData />
        </BrokerHeader>
      </soap:Header>
      <soap:Body>
        <Login xmlns="http://NBK.com/Security" />
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>