	<xsl:for-each select="/*/rdDataID">
    <SPAN style="display:none;" rdActionMapMarkerInfo="bActionMapMarkerInfo">
      <xsl:attribute name="id">
        <xsl:value-of select="concat('rdDataID_Row',position())"/>
      </xsl:attribute>
      rdMarkerText
      rdMarkerImage
      rdMarkerInfo
      rdMarkerAction
    </SPAN>
    </xsl:for-each>

