namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public enum ObjectType
	{
		Null,
		None,
		RIFObjectArray,
		RIFObjectList,
		PrimitiveArray,
		PrimitiveList,
		PrimitiveTypedArray,
		StringRIFObjectDictionary,
		StringRIFObjectHashtable,
		NameObjectCollection,
		Int32RIFObjectDictionary,
		Int32PrimitiveListHashtable,
		ObjectHashtableHashtable,
		StringObjectHashtable,
		ListOfRIFObjectRIFObjectDictionary,
		RIFObjectStringHashtable,
		RecordSetInfo,
		RecordRow,
		RecordField,
		RecordSetPropertyNames,
		Nullable,
		StorageItem,
		Reference,
		ScalableDictionaryNodeReference,
		IScalableDictionaryEntry,
		ScalableDictionaryValues,
		ScalableDictionaryNode,
		ScalableDictionary,
		StorableArray,
		StorableArrayReference,
		ScalableList,
		Array2D,
		DataRegionInstanceReference,
		SubReportInstanceReference,
		ReportInstanceReference,
		ScopeInstanceReference,
		DataFieldRow,
		FieldImpl,
		BTreeNode,
		BTreeNodeTupleList,
		BTreeNodeTuple,
		BTreeNodeValue,
		ScalableHybridListEntry,
		FilterKey,
		RuntimeSortHierarchyObj,
		SortHierarchyStruct,
		RuntimeSortFilterEventInfo,
		SortFilterExpressionScopeObj,
		SortExpressionScopeInstanceHolder,
		DataAggregateObj,
		RuntimeRICollection,
		RuntimeTablixCell,
		RuntimeChartCriCell,
		RuntimeUserSortTargetInfo,
		Aggregate,
		First,
		Last,
		Sum,
		Avg,
		Max,
		Min,
		Count,
		VariantVariantHashtable,
		CountDistinct,
		CountRows,
		VarBase,
		Var,
		StDev,
		VarP,
		StDevP,
		Previous,
		AggregateRow,
		RuntimeCells,
		RuntimeExpressionInfo,
		Int32StringHashtable,
		RuntimeHierarchyObj,
		RuntimeGroupingObj,
		VariantRifObjectDictionary,
		VariantListOfRifObjectDictionary,
		AggregatesImpl,
		RuntimeDataTablixGroupRootObj,
		RuntimeDataTablixMemberObj,
		RuntimeTablixMemberObj,
		RuntimeDataTablixObj,
		RuntimeTablixObj,
		RuntimeChartObj,
		RuntimeCriObj,
		RuntimeTablixGroupLeafObj,
		RuntimeChartCriGroupLeafObj,
		CalculatedFieldWrapperImpl,
		RuntimeSortDataHolder,
		VariantListVariantDictionary,
		BTree,
		DataFieldRowReference,
		RuntimeSortHierarchyObjReference,
		RuntimeSortFilterEventInfoReference,
		SortFilterExpressionScopeObjReference,
		SortExpressionScopeInstanceHolderReference,
		DataAggregateObjReference,
		RuntimeRICollectionReference,
		RuntimeTablixCellReference,
		RuntimeChartCriCellReference,
		RuntimeUserSortTargetInfoReference,
		AggregateRowReference,
		RuntimeCellsReference,
		RuntimeHierarchyObjReference,
		RuntimeGroupingObjReference,
		RuntimeDataTablixGroupRootObjReference,
		RuntimeDataTablixMemberObjReference,
		RuntimeTablixMemberObjReference,
		RuntimeTablixObjReference,
		RuntimeChartObjReference,
		RuntimeCriObjReference,
		RuntimeTablixGroupLeafObjReference,
		RuntimeChartCriGroupLeafObjReference,
		RuntimeSortDataHolderReference,
		StringVariantListDictionary,
		RowMemberInfo,
		SizeInfo,
		DetailCell,
		CornerCell,
		MemberCell,
		PageMemberCell,
		BTreeNodeHierarchyObj,
		BTreeNodeDataRow,
		DataCellInstanceList,
		IDOwner = 0x80,
		ReportItem,
		Report,
		PageSection,
		Line,
		Rectangle,
		Image,
		TextBox,
		SubReport,
		DataRegion,
		ReportHierarchyNode,
		Grouping,
		Sorting,
		ReportItemCollection,
		ReportItemIndexer,
		Style,
		AttributeInfo,
		Visibility,
		ExpressionInfo,
		DataAggregateInfo,
		RunningValueInfo,
		Filter,
		DataSource,
		DataSet,
		ReportQuery,
		Field,
		ParameterValue,
		ReportSnapshot,
		DocumentMapNode,
		InstanceInfo,
		ScopeInstance,
		ReportInstance,
		ParameterInfo,
		ParameterInfoCollection,
		Variant,
		VariantList,
		ValidValue,
		ParameterDataSource,
		ParameterDef,
		ParameterBase,
		ProcessingMessageList,
		ProcessingMessage,
		CodeClass,
		String,
		Action,
		RenderingPagesRanges,
		IntermediateFormatVersion,
		ImageInfo,
		ActionItem,
		DataValue,
		CustomReportItem,
		SortFilterEventInfo,
		SortFilterEventInfoMap,
		EndUserSort,
		ISortFilterScope,
		GroupingList,
		ScopeLookupTable,
		Row,
		Cell,
		Tablix,
		TablixHeader,
		TablixMember,
		TablixColumn,
		TablixRow,
		TablixCornerCell,
		TablixCell,
		Chart,
		ChartMember,
		ChartSeries,
		ChartDataPoint,
		ChartAxis,
		AxisList,
		ThreeDProperties,
		PlotArea,
		ChartDataLabel,
		ChartDataPointValues,
		ChartArea,
		ChartTitleBase,
		ChartTitle,
		ChartAxisTitle,
		ChartLegendTitle,
		ChartLegend,
		ChartBorderSkin,
		ChartTickMarks,
		ChartNoDataMessage,
		ChartCustomPaletteColor,
		ChartLegendColumn,
		ChartLegendColumnHeader,
		ChartLegendCustomItem,
		ChartLegendCustomItemCell,
		ChartStripLine,
		ChartAxisScaleBreak,
		ChartDerivedSeries,
		ChartFormulaParameter,
		ChartEmptyPoints,
		ChartItemInLegend,
		ChartSmartLabel,
		ChartNoMoveDirections,
		GridLines,
		DataMember,
		CustomDataRow,
		DataCell,
		Variable,
		ExpressionInfoTypeValuePair,
		Page,
		IReferenceable,
		SubReportInstance,
		SubReportInstanceItem,
		Parameter,
		CultureInfo,
		Declaration,
		DocumentMapBeginContainer,
		DocumentMapEndContainer,
		OnDemandMetadata,
		GroupTreePartition,
		FieldInfo,
		DataSetInstance,
		DataRegionInstance,
		DataRegionMemberInstance,
		DataCellInstance,
		DataAggregateObjResult,
		Parameters,
		StringInt32Hashtable,
		Variables,
		SubReportInfo,
		StringStringHashtable,
		ChartStyleContainer,
		ChartMarker,
		IInScopeEventSource,
		IVisibilityOwner,
		ReportElementInstance,
		ReportItemInstance,
		ImageInstance,
		ActionInstance,
		ParameterInstance,
		ActionInfoWithDynamicImageMap,
		ImageMapAreaInstance,
		StyleInstance,
		StringListOfStringDictionary,
		ChartAlignType,
		RIFObject,
		OnDemandProcessingContext,
		ObjectModelImpl,
		RuntimeOnDemandDataSetObj,
		ISortDataHolder,
		IHierarchyObj,
		RuntimeRDLDataRegionObj,
		RuntimeCell,
		Filters,
		ReportRuntime,
		DataAggregate,
		IScope,
		IndexedExprHost,
		RuntimeGroupLeafObj,
		RuntimeGroupObj,
		RuntimeDetailObj,
		IErrorContext,
		RuntimeGroupRootObj,
		RuntimeMemberObj,
		RuntimeChartCriObj,
		RuntimeDataTablixGroupLeafObj,
		RuntimeOnDemandDataSetObjReference,
		IHierarchyObjReference,
		RuntimeCellReference,
		RuntimeRDLDataRegionObjReference,
		IScopeReference,
		RuntimeDataTablixGroupLeafObjReference,
		RuntimeGroupLeafObjReference,
		RuntimeGroupObjReference,
		RuntimeDetailObjReference,
		RuntimeGroupRootObjReference,
		RuntimeMemberObjReference,
		RuntimeDataTablixObjReference,
		RuntimeChartCriObjReference,
		ISortDataHolderReference,
		RuntimeDataRegionObjReference,
		RuntimeDataRegionObj,
		DataAggregateReference,
		StreamMemberCell,
		RPLMemberCell,
		ItemSizes,
		PageItem,
		HiddenPageItem,
		NoRowsItem,
		PageItemContainer,
		ReportBody,
		RowInfo,
		ColumnInfo,
		PageTablixCell,
		PageDetailCell,
		PageCornerCell,
		PageStructMemberCell,
		PageStructStaticMemberCell,
		PageStructDynamicMemberCell,
		CommonSubReportInfo,
		TablixCellBase,
		RuntimeGaugePanelObj,
		RuntimeGaugePanelObjReference,
		GaugePanel,
		GaugeMember,
		GaugeRow,
		GaugeCell,
		GaugePanelStyleContainer,
		FrameBackground,
		BaseGaugeImage,
		IndicatorImage,
		PointerImage,
		CapImage,
		FrameImage,
		CustomLabel,
		Gauge,
		RadialGauge,
		LinearGauge,
		GaugeImage,
		GaugeLabel,
		GaugePanelItem,
		GaugePointer,
		RadialPointer,
		LinearPointer,
		GaugeScale,
		RadialScale,
		LinearScale,
		GaugeTickMarks,
		TickMarkStyle,
		ScalePin,
		GaugeInputValue,
		NumericIndicator,
		PinLabel,
		PointerCap,
		ScaleLabels,
		ScaleRange,
		StateIndicator,
		BackFrame,
		TopImage,
		Thermometer,
		DynamicImage,
		ExcelRowInfo,
		IRowItemStruct,
		RowItemStruct,
		TablixItemStruct,
		TablixStruct,
		TablixMemberStruct,
		ToggleParent,
		ChildLeafInfo,
		TextOrientation,
		AspectRatio,
		ChartElementPosition,
		Paragraph,
		TextRun,
		ByteVariantHashtable,
		TextBoxOffset,
		StringBoolArrayDictionary,
		LookupInfo,
		LookupDestinationInfo,
		LookupTable,
		LookupTableReference,
		LookupObjResult,
		LookupMatches,
		LookupMatchesWithRows,
		TreePartitionManager,
		ReportSection,
		RuntimeMapDataRegionObj,
		RuntimeMapDataRegionObjReference,
		Map,
		MapDataRegion,
		MapMember,
		MapRow,
		MapCell,
		MapStyleContainer,
		MapLocation,
		MapSize,
		MapGridLines,
		MapSubItem,
		MapDockableSubItem,
		MapBindingFieldPair,
		MapViewport,
		MapLimits,
		MapColorScale,
		MapColorScaleTitle,
		MapDistanceScale,
		MapTitle,
		MapLegend,
		MapLegendTitle,
		MapAppearanceRule,
		MapBucket,
		MapColorPaletteRule,
		MapColorRangeRule,
		MapColorRule,
		MapLineRules,
		MapPolygonRules,
		MapSizeRule,
		MapMarkerImage,
		MapMarker,
		MapMarkerRule,
		MapPointRules,
		MapCustomColor,
		MapCustomColorRule,
		MapLineTemplate,
		MapPolygonTemplate,
		MapMarkerTemplate,
		MapPointTemplate,
		MapSpatialElementTemplate,
		MapField,
		MapLine,
		MapPolygon,
		MapSpatialElement,
		MapPoint,
		MapFieldDefinition,
		MapFieldName,
		MapLayer,
		MapLineLayer,
		MapShapefile,
		MapPolygonLayer,
		MapSpatialData,
		MapSpatialDataRegion,
		MapSpatialDataSet,
		MapPointLayer,
		MapTile,
		MapTileLayer,
		MapVectorLayer,
		MapBorderSkin,
		MapCustomView,
		MapDataBoundView,
		MapElementView,
		MapView,
		ShapefileInfo,
		Union,
		PageBreak,
		PageBreakProperties,
		UpdatedVariableValues,
		Int32SerializableDictionary,
		SerializableArray,
		DataScopeInfo,
		BucketedDataAggregateInfos,
		DataAggregateInfoBucket,
		BucketedDataAggregateObjs,
		DataAggregateObjBucket,
		NumericIndicatorRange,
		IndicatorState,
		BucketedRunningValueInfos,
		RunningValueInfoBucket,
		SharedDataSetQuery,
		DataSetCore,
		DataSetParameterValue,
		RIFVariantContainer,
		NLevelVariantHashtable,
		SortScopeValuesHolder,
		RuntimeDataRowSortHierarchyObj,
		SyntheticTriangulatedCellReference,
		RuntimeGroupingObjHash,
		RuntimeGroupingObjTree,
		RuntimeGroupingObjDetail,
		RuntimeGroupingObjLinkedList,
		RuntimeGroupingObjDetailUserSort,
		RuntimeGroupingObjNaturalGroup,
		Relationship,
		IdcRelationship,
		DefaultRelationship,
		JoinCondition,
		BandLayoutOptions,
		LabelData,
		Slider,
		Coverflow,
		Navigation,
		PlayAxis,
		BandNavigationCell,
		Tabstrip,
		NavigationItem,
		ScopeIDInfo,
		WordOpenXmlTableRowProperties,
		WordOpenXmlBorderProperties,
		WordOpenXmlBaseInterleaver,
		WordOpenXmlTableGrid,
		WordOpenXmlHeaderFooterReferences,
		StreamingNoRowsDataRegionInstance,
		StreamingNoRowsCellInstance,
		StreamingNoRowsMemberInstance,
		SyntheticOnDemandScopeInstanceReference,
		SyntheticOnDemandDataRegionInstanceReference,
		SyntheticOnDemandMemberInstanceReference,
		JoinInfo,
		LinearJoinInfo,
		IntersectJoinInfo,
		RdlFunctionInfo,
		ProcessingComparer,
		ScopedFieldInfo,
		RuntimeCellWithContents,
		RuntimeDataTablixWithScopedItemsGroupLeafObj,
		RuntimeDataTablixWithScopedItemsObj,
		RuntimeDataShapeObj,
		RuntimeDataShapeGroupLeafObj,
		RuntimeDataShapeIntersection,
		RuntimeDataShapeObjReference,
		RuntimeDataShapeGroupLeafObjReference,
		RuntimeDataShapeIntersectionReference,
		ParametersLayout,
		ParameterGridLayoutCellDefinition,
		MaxValue
	}
}
