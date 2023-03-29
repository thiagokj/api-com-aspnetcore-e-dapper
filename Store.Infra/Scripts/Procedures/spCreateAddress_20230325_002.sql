CREATE PROCEDURE spCreateAddress
    @Id UNIQUEIDENTIFIER,
	@CustomerId UNIQUEIDENTIFIER,
	@Street VARCHAR(50),
	@Number VARCHAR(10),
	@Complement VARCHAR(40),
	@Neighborhood VARCHAR(60),
	@City VARCHAR(60),
	@State CHAR(2),
	@Country CHAR(2),
	@ZipCode CHAR(8),
	@Type INT
AS
    INSERT INTO [Address] (
        [Id],
        [CustomerId],
        [Street],
        [Number],
        [Complement],
        [Neighborhood],
        [City],
        [State],
        [Country],
        [ZipCode],
        [Type]
    ) VALUES (
        @Id,
        @CustomerId,
        @Street,
        @Number,
        @Complement,
        @Neighborhood,
        @City,
        @State,
        @Country,
        @ZipCode,
        @Type
    )