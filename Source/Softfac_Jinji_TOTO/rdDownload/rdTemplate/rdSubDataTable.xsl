
<TableRows>
	<xsl:for-each select="rdDataID" >
		<xsl:variable name="rdDataTableID-Position" select="position()"/>
			<rdTableRows />
	</xsl:for-each>
</TableRows>
