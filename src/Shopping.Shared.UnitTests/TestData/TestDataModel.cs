using Shopping.Shared.Model.Serialization;
using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Shared.Enums;
using Shopping.Shared.Model.Account;

namespace Shopping.Shared.UnitTests.TestData
{
    public class TestDataModel
    {
        public int ExpectedCategoryCount;
        public int ExpectedProductCount;
        public int ExpectedShoppingListCount;
        public int ExpectedUserGroupCount;
        public int ExpectedAssignmentCount;

        public ShoppingDataSerializationModel Data;
    }

    public static class TestDataRepo
    {
        public static TestDataModel GetDataSet()
        {
            return new TestDataModel
            {
                ExpectedCategoryCount = 20,
                ExpectedProductCount = 146,
                ExpectedShoppingListCount = 5,
                ExpectedUserGroupCount = 1,
                ExpectedAssignmentCount = 5,
                Data = new ShoppingDataSerializationModel
                {
                    Categories = new List<ProductCategory>
                   {
                        new ProductCategory 
                        {
                          Name= "Fleisch",
                          Id= "f3c6cc51-fcbe-4056-9c35-a1ed055ca9f0",
                          CreatedAt= DateTime.Parse("2020-07-03T12:34:43.0569932+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Gemüse",
                          Id = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0586309+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Obst",
                          Id = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0603093+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Süßigkeit",
                          Id = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0603529+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Chips",
                          Id = "6a1c39dd-7a45-4bb5-ae0e-ba0aa99d139f",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604165+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Milchprodukte ",
                          Id = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604248+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Sauce",
                          Id = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604306+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Konserven",
                          Id = "bab91259-2b36-4f62-871c-857f8e2bef95",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604361+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Backwaren",
                          Id = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604459+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Teigwaren",
                          Id = "55f92dfd-b3a8-435c-a668-ef5235174372",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604518+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Getränke",
                          Id = "80322c66-7733-47af-9045-2b8650993490",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0604593+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Wurst",
                          Id = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0647508+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Hygiene",
                          Id = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0648154+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Reinigungsmittel",
                          Id = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0648212+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Eier",
                          Id = "e731a8d0-3996-4711-9e1b-096d893d8c78",
                          CreatedAt = DateTime.Parse( "2020-07-03T12:34:43.0649092+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Küche",
                          Id = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                          CreatedAt = DateTime.Parse( "2020-06-16T19:35:04.887+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Tiefkühlgericht",
                          Id = "42b96200-8d14-4d43-9ef4-02cc372c3e08",
                          CreatedAt = DateTime.Parse( "2020-06-21T18:56:23.265+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Baby",
                          Id = "406728b4-545e-4182-94fa-f79fad0c02a1",
                          CreatedAt = DateTime.Parse( "2020-06-21T20:12:18.491+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Gewürze",
                          Id = "9048b819-c949-4fb3-843b-372c6900b336",
                          CreatedAt = DateTime.Parse( "2020-06-22T19:24:24.375+02:00"),
                        },
                        new ProductCategory
                        {
                          Name = "Nüsse",
                          Id = "543a909f-f689-4046-a44e-c3e1bb8f9c09",
                          CreatedAt = DateTime.Parse( "2020-06-25T18:15:51.808+02:00"),
                        }
                    },
                    Products = new List<ProductItem>
                    {
                        new ProductItem
                        {
                            Name = "Blätterteig",
                            Unit = (ProductUnit) 4,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "3211d9fa-df8b-4f03-987d-61c0db7236d6",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0623989+02:00"),
                            },
                            Id = "0020722e-eeb6-4a39-9d96-587aefad2911",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0623963+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Toast",
                            Unit = (ProductUnit) 4,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "2e444d07-2243-48fb-8177-abc1db15520d",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642406+02:00"),
                            },
                            Id = "089f24cc-9110-4df3-a1e7-8831776fdd83",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642383+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Erdbeeren",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "18bc32a6-5821-46ce-93d9-269c81cefd92",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642589+02:00"),
                            },
                            Id = "d437c7a4-85ef-4d3d-83f9-c97d9fc28628",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642573+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Banane",
                            Unit = (ProductUnit) 3,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "9e4e8579-b18d-4e5a-8f90-f396aa9c294b",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064267+02:00"),
                            },
                            Id = "7d0660cd-b813-48e6-b553-648048fb0034",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642663+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Baguette",
                            Unit = (ProductUnit) 3,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "886e989b-4bc2-4f8f-8d56-2bf9af6c68de",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642781+02:00"),
                            },
                            Id = "36d64c94-d454-42d6-888d-7e9411105868",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642774+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Weintrauben",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "ec948eaa-9bc7-4152-a4b0-d251935efc1c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642855+02:00"),
                            },
                            Id = "fb5179b3-1192-47f3-a731-4ae1d335fce6",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642848+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Orangen",
                            Unit = (ProductUnit) 2,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "6151fd9f-2336-4dae-9d3a-4d4842e788c5",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642925+02:00"),
                            },
                            Id = "e480d797-5c16-4269-8868-ed94586c67b9",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642915+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Nudel",
                            Unit = (ProductUnit) 1,
                            CategoryId = "55f92dfd-b3a8-435c-a668-ef5235174372",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "7e1327c8-ba61-4214-96f0-1b41263aff26",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642994+02:00"),
                            },
                            Id = "aec781dd-b323-470e-a7dc-177d767bda3d",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0642988+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Maultasche",
                            Unit = (ProductUnit) 1,
                            CategoryId = "55f92dfd-b3a8-435c-a668-ef5235174372",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "e723ccd4-fdd9-4430-9455-7565c846caa2",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643093+02:00"),
                            },
                            Id = "79da55a8-29da-49f4-bba9-aa2e31903c73",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643086+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Tortellini",
                            Unit = (ProductUnit) 1,
                            CategoryId = "55f92dfd-b3a8-435c-a668-ef5235174372",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "11a43fb3-b312-40b5-bd00-2881fc85b621",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643162+02:00"),
                            },
                            Id = "aa3bed2b-760a-4eb9-8736-00f2c85ae8a2",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643155+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Gnocchi",
                            Unit = (ProductUnit) 1,
                            CategoryId = "55f92dfd-b3a8-435c-a668-ef5235174372",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "cb4119e4-f731-459e-87f0-98a8aafc2f90",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643234+02:00"),
                            },
                            Id = "11299708-f6f1-4fca-8ee2-1b80bc22d940",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643223+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Chio Tortillas",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6a1c39dd-7a45-4bb5-ae0e-ba0aa99d139f",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "65aaeef7-d5c7-4f50-a1b3-77caba4701db",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643327+02:00"),
                            },
                            Id = "1d65f833-7266-403b-b001-c646e54da161",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643295+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Chips ungarisch",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6a1c39dd-7a45-4bb5-ae0e-ba0aa99d139f",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "d3de94f2-f634-4a15-a4cf-2e8e785542d5",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643397+02:00"),
                            },
                            Id = "45b3a6df-26f4-4058-8fcb-0ad296372187",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643391+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Chips mit salz",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6a1c39dd-7a45-4bb5-ae0e-ba0aa99d139f",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "8ba613b9-0a9e-457f-a57b-b19ab4d155f6",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643465+02:00"),
                            },
                            Id = "fe02d954-46f0-4689-94ed-55abd26d97a4",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643458+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Doritos",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6a1c39dd-7a45-4bb5-ae0e-ba0aa99d139f",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "ae36cb1b-47ea-45ed-bbd4-964152b57993",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643535+02:00"),
                            },
                            Id = "2c82de43-221a-4560-9fa7-925acc191e21",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643525+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Hackfleisch",
                            Unit = (ProductUnit) 1,
                            CategoryId = "f3c6cc51-fcbe-4056-9c35-a1ed055ca9f0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c5614f22-8442-4055-9406-dda55c912d14",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643631+02:00"),
                            },
                            Id = "256a2a24-1241-4603-b0fe-e783578c9e0f",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643625+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Grill Fleisch",
                            Unit = (ProductUnit) 1,
                            CategoryId = "f3c6cc51-fcbe-4056-9c35-a1ed055ca9f0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "4b9a4b71-8aca-41ce-b270-77cee5e07779",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.06437+02:00"),
                            },
                            Id = "bef260f2-0662-4a83-9081-b1d2cec9e145",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643693+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Paprika",
                            Unit = (ProductUnit) 3,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "5dcfe817-005e-430c-93d9-055b4c5bc8a5",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643796+02:00"),
                            },
                            Id = "a31cb155-8eaf-4bda-a8e3-decca7156503",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643764+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Zwiebeln",
                            Unit = (ProductUnit) 2,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "fc589f38-49fd-489e-ba1d-4d5bafc00961",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643871+02:00"),
                            },
                            Id = "70ebe5c0-c4f4-4590-b584-3b07da0a6522",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643861+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Knoblauch",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "d6a6211a-fb49-4bc0-8505-6466af64fd4d",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064394+02:00"),
                            },
                            Id = "b5d9d45b-c0c9-42a0-9e80-cfb64c16d391",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0643933+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Karotten",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "f7b85a26-8b34-43c2-aad5-d48a293da786",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644042+02:00"),
                            },
                            Id = "4f8b9226-c9c1-4c6c-8f8f-1d5b86e59c72",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644035+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kartoffeln",
                            Unit = (ProductUnit) 2,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "7e9cbb71-968c-4094-85af-8039017887e9",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644112+02:00"),
                            },
                            Id = "9af57e07-b21d-497f-9aaf-36fc4ac311c6",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644106+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Sellerie",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "976a6541-3188-42bf-89af-5dccf578fca3",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644184+02:00"),
                            },
                            Id = "fd421372-99e8-4359-953b-240e6a82843d",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644175+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Tomaten",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "68de3545-8f9b-472b-ae4a-4e46ac660c73",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064428+02:00"),
                            },
                            Id = "8ab8794f-9f73-465e-89af-3ce1361bbfd4",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644274+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Champignons",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c15dec86-f7d0-4e72-90d2-bad29b8ff3a4",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644351+02:00"),
                            },
                            Id = "16c1f831-1850-4995-918b-ea8f51b1e831",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644344+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Brokkoli",
                            Unit = (ProductUnit) 3,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "de7c7a5a-d6c8-4598-a6cd-71278bd9fd4c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064447+02:00"),
                            },
                            Id = "66114c23-cb95-4d4d-a397-9471e6aea730",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644463+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Erbsen",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "eadb72e0-babf-495c-9a42-e64325187ef6",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644546+02:00"),
                            },
                            Id = "fbafa613-1687-4dd3-9791-74c3c30b8820",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644536+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Putenfleisch",
                            Unit = (ProductUnit) 1,
                            CategoryId = "f3c6cc51-fcbe-4056-9c35-a1ed055ca9f0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "4bf7f3c5-12a1-402c-892e-76ccb4eb2458",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644616+02:00"),
                            },
                            Id = "eb5d7933-c875-4333-8360-c9140ed050e1",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644609+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Frischmilch ",
                            Unit = (ProductUnit) 6,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "2d80f632-9ad1-48b7-977c-c5f62e2334d7",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644712+02:00"),
                            },
                            Id = "80c7afa1-214d-4a4c-92b6-17e13636a8d5",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644706+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Hafermilch",
                            Unit = (ProductUnit) 6,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "73083900-e706-471d-860f-5cf2e5fc6583",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644784+02:00"),
                            },
                            Id = "c75a10d5-1047-4efb-806b-2320604d71bd",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644778+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Geriebener Käse",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c8ada75e-3815-4dfe-bd11-285666f89f92",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064491+02:00"),
                            },
                            Id = "5f1e3d16-cae8-48c5-96b3-347ff36f3766",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.06449+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Mozzarella",
                            Unit = (ProductUnit) 3,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "9878d3a3-609d-4ed5-acb6-787335d639a5",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644986+02:00"),
                            },
                            Id = "0e6cc8fd-2436-4881-a864-eb2cd6b52b2c",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0644979+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Burrata",
                            Unit = (ProductUnit) 3,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c57b50da-3e03-4d71-b7f6-f2432ce6fd8a",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645088+02:00"),
                            },
                            Id = "24099a69-c0fc-45c6-b00a-6cfdafdde339",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645081+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Parmesan",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "2319ba75-e01a-433b-a98b-12fe1f6c5cce",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645165+02:00"),
                            },
                            Id = "5938480d-24c0-4d84-a695-201db56a543f",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645158+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Käsescheiben",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "2788531e-a53b-4251-abf6-d201cad87250",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645339+02:00"),
                            },
                            Id = "23213305-ebd3-45ee-aec4-2c483c34c2a4",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645328+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Tee",
                            Unit = (ProductUnit) 4,
                            CategoryId = "80322c66-7733-47af-9045-2b8650993490",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "cef93deb-baba-4297-9659-41576412fc30",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645419+02:00"),
                            },
                            Id = "7fa643ea-519c-4a9a-8bd4-a59a8e3db99a",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645412+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kinder Riegel",
                            Unit = (ProductUnit) 3,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "5d7eebc0-4415-48c7-91ee-4741a4611a94",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645527+02:00"),
                            },
                            Id = "162b0090-0ce7-41d0-b284-5df1d43fa7ad",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064552+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Schoko-Bons ",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "bf6b4374-6fb1-4420-80c3-6a250607f7b6",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645602+02:00"),
                            },
                            Id = "7f87ae24-95d8-4976-baa3-32cc2c6c6dee",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645596+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Milka",
                            Unit = (ProductUnit) 3,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "3b775be0-a0b2-4790-b030-aa8f441cc2df",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645705+02:00"),
                            },
                            Id = "636325c3-c3e5-4ad0-989e-7efa491385b0",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645696+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Giotto",
                            Unit = (ProductUnit) 3,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "dd318e90-11d7-4c9a-ad2f-4f20e3eeb313",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064578+02:00"),
                            },
                            Id = "2ba5e773-cd85-46c5-9882-5be80f0a6eac",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645774+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Raffaello",
                            Unit = (ProductUnit) 3,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "a9ccdde7-d8dc-491a-bdc3-305f94d061d4",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645883+02:00"),
                            },
                            Id = "72d06a7d-9275-476b-ac78-114c9bbbc012",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645875+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kinder Bueno",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "f73e1187-1fd8-43f7-be03-e070187c7203",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645958+02:00"),
                            },
                            Id = "c291444c-49ab-40cc-b081-2a3e938d0684",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0645952+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Yogurette",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "871d64e6-a6a8-4783-a2e9-0d12bae2d205",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646082+02:00"),
                            },
                            Id = "ad83ceb6-a642-4316-bc24-1978927b213b",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646072+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kinder Cards",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "1d6fe6b9-f625-4afa-bb07-9ce527a20ccc",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646158+02:00"),
                            },
                            Id = "5370998f-c62b-4f8a-a928-fbd7e45d4288",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646151+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Mais",
                            Unit = (ProductUnit) 7,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "c1aaaff5-fc14-4509-a5c8-4ed5125802b4",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646262+02:00"),
                            },
                            Id = "303726e7-c095-45f9-aad8-88511a6269d7",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646255+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kidneybohnen",
                            Unit = (ProductUnit) 7,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "c295f161-90f8-4b51-8df4-8e5be3e6b2f9",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646338+02:00"),
                            },
                            Id = "03653ac5-7b43-44e8-8be6-33029fac5f4d",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646332+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Marmelade",
                            Unit = (ProductUnit) 8,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "9d65715a-bb7b-478e-b751-1eeeefa33177",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646444+02:00"),
                            },
                            Id = "c7cb42c3-a93a-4077-8a7d-6e5d18749133",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646435+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Pesto",
                            Unit = (ProductUnit) 8,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "68700b6f-bd74-4c18-b7ca-2e4e94e63b52",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646549+02:00"),
                            },
                            Id = "ec3a5a68-2dcc-4503-bf0f-eebde568bea1",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646542+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Coca-Cola",
                            Unit = (ProductUnit) 9,
                            CategoryId = "80322c66-7733-47af-9045-2b8650993490",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "08488b1d-d5c5-4952-9c1b-1953a60edf30",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646627+02:00"),
                            },
                            Id = "9e4512ee-f25b-4dea-bae5-8bbbb253ca5a",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646621+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Tonic Water",
                            Unit = (ProductUnit) 9,
                            CategoryId = "80322c66-7733-47af-9045-2b8650993490",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "2d0d32e0-b183-4014-99a9-6c5c2f89fdc5",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646729+02:00"),
                            },
                            Id = "73285021-f3db-451d-a1a0-13395fc9ae70",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646723+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Saft",
                            Unit = (ProductUnit) 9,
                            CategoryId = "80322c66-7733-47af-9045-2b8650993490",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "4d4ab49c-e4f9-433e-be95-43a04be2bc27",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646809+02:00"),
                            },
                            Id = "e384d1a1-f055-4f3c-bc13-4b86448dfd40",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.06468+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Pesto alla calabrese",
                            Unit = (ProductUnit) 8,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "bb6ce9ff-8b7e-46ea-9a2b-859b5612060f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646914+02:00"),
                            },
                            Id = "616784c5-c38e-447b-932f-801c46975088",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0646907+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Schlagsahne",
                            Unit = (ProductUnit) 5,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "fc8aebaa-0402-4c4e-a86c-c858a0b3ad72",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647021+02:00"),
                            },
                            Id = "09da2ba2-491d-4379-9754-23600e373df6",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647014+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Crême fresch",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "56ae52ea-f31f-4fcb-92f5-fa30bd937a1f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647123+02:00"),
                            },
                            Id = "44ffce36-5171-475a-b001-d15fb74810bd",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647116+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Frischkäse",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "4cac6f1c-b3f2-455f-aaa7-30b678ec2fe4",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647254+02:00"),
                            },
                            Id = "16f32fdb-ae19-43d0-9c7b-74440f742ba6",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647244+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kräuter Frischkäse",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "c4eb6151-05b8-4061-b3cf-846e4f7d18b8",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647424+02:00"),
                            },
                            Id = "b7e40319-289a-4406-9538-120aa4c4a7bc",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647353+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Putenbrust",
                            Unit = (ProductUnit) 4,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "1178f79d-a4e3-4fcf-a387-0c9af9ca90c3",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647577+02:00"),
                            },
                            Id = "677aa76e-3160-4484-9fb8-d06ab9171bc2",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647571+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Salami",
                            Unit = (ProductUnit) 4,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "b09b79e2-945a-472f-9bf7-4f492a5677ab",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647688+02:00"),
                            },
                            Id = "2e2f482a-4d92-45db-a220-6a6f5529039f",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064768+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Mortadella",
                            Unit = (ProductUnit) 4,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "9ca4f77e-81d0-49a5-abf4-1286e98d321b",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647804+02:00"),
                            },
                            Id = "9b60f954-1eaa-4e4e-acb4-da1bbc9bfb56",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647797+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Gekochter Schinken",
                            Unit = (ProductUnit) 4,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "7ff6ca3b-9a64-44b3-9b8c-80d88669aa97",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647885+02:00"),
                            },
                            Id = "395efffc-37ac-4170-81ba-5ccf76623744",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647878+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Leberwurst",
                            Unit = (ProductUnit) 4,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "97dad94d-9528-4295-98c6-aa42070e8435",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647987+02:00"),
                            },
                            Id = "e5bde830-9d67-4b1e-a49c-3c22a5a07448",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0647981+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Tomatensoße",
                            Unit = (ProductUnit) 4,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "962ad2ec-11b9-471e-a420-a3bf6869dd0f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648083+02:00"),
                            },
                            Id = "b6e4ec34-2065-4429-be9a-fd95602e2c7c",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648076+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Zahnbürste",
                            Unit = (ProductUnit) 4,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "08b845f2-c306-44a2-8a85-34b05770e329",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648296+02:00"),
                            },
                            Id = "625e63a4-9b04-4f2f-9bd1-11690910139a",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064829+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Zahnpasta",
                            Unit = (ProductUnit) 4,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "a49602d0-ed70-46af-b24b-4eb9e2aa2998",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648404+02:00"),
                            },
                            Id = "96d40ed8-aa88-4716-9584-bffcdfd16568",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648398+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kukident die Blauen",
                            Unit = (ProductUnit) 4,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "e8a09ec7-c7fb-4997-a42a-5913e2240e5e",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064848+02:00"),
                            },
                            Id = "27dd4310-dda3-4373-bfe8-8fe390e6c5d0",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648473+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Listerine",
                            Unit = (ProductUnit) 9,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "26402a76-1628-42f6-bc61-1af6f05dd66e",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648565+02:00"),
                            },
                            Id = "abbc300a-ff6b-4c17-af6c-e8573552345e",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648559+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Deodorant",
                            Unit = (ProductUnit) 9,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "c6604d5c-ec1c-4730-85a0-82ff20947d34",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648639+02:00"),
                            },
                            Id = "deb79b46-ec74-4c9d-938c-acbad9777edc",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648633+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Duschgel",
                            Unit = (ProductUnit) 9,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "1ae66934-5daf-43d2-9594-018daba49de6",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648712+02:00"),
                            },
                            Id = "c61db7f1-e688-48ec-848b-f3878a5f4d26",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648706+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Head \u0026 Shoulders P.",
                            Unit = (ProductUnit) 9,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "14f64696-6ab0-49ae-bc16-70a20654fe97",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648786+02:00"),
                            },
                            Id = "e07541a3-4df2-49e0-972c-90fe43dddcd6",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648779+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Head \u0026 Shoulders Z.",
                            Unit = (ProductUnit) 9,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "3532a0c1-526f-4ad7-a6a5-6c1ea6a856b8",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648904+02:00"),
                            },
                            Id = "8ed75f2a-9c69-4053-8b86-95c7781a98de",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0648896+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Spülmittel",
                            Unit = (ProductUnit) 9,
                            CategoryId = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "450a70bc-8e50-4ea5-8056-79d335563b14",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0649013+02:00"),
                            },
                            Id = "0f5e5f3c-7360-4982-b525-5dca9727078f",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0649006+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Eier aus Bodenhaltung",
                            Unit = (ProductUnit) 4,
                            CategoryId = "e731a8d0-3996-4711-9e1b-096d893d8c78",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "18d28213-4153-41b7-9281-93e143e58f74",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0649168+02:00"),
                            },
                            Id = "6e4b6f9e-ef8c-4eac-9ad7-99bbf04f3284",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064916+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Eiweiß",
                            Unit = (ProductUnit) 5,
                            CategoryId = "e731a8d0-3996-4711-9e1b-096d893d8c78",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "f18ba630-228f-414f-b1ce-e49a3d54d86d",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.064928+02:00"),
                            },
                            Id = "5274ca86-0267-4808-bca4-eb24efbfa6cc",
                            CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0649269+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Griechischer Joghurt",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "5103bce7-3df3-4d80-88e9-9bbf5eefe424",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0778673+02:00"),
                            },
                            Id = "633fcf92-65f3-47d5-90db-c353286d2122",
                            CreatedAt = DateTime.Parse("2020-06-16T18:53:48.257+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kichererbsen",
                            Unit = (ProductUnit) 7,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "ec6b2591-92e4-4cb9-8c41-24be089716c1",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0780468+02:00"),
                            },
                            Id = "74a27f47-e1d2-43a6-b84e-e4fcc3696e72",
                            CreatedAt = DateTime.Parse("2020-06-16T19:03:32.937+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Baked Bohnen",
                            Unit = (ProductUnit) 7,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "27aff3be-48ae-43e5-bbb6-592259b9c58c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0780598+02:00"),
                            },
                            Id = "e22bbc71-0efc-48fd-9e71-50405afc3e9b",
                            CreatedAt = DateTime.Parse("2020-06-16T19:09:57.592+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Müsli",
                            Unit = (ProductUnit) 4,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "35bc8c7d-0cf5-4f26-b4b0-236415ef9660",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0780728+02:00"),
                            },
                            Id = "7ac870d7-1209-486b-8faf-9b4633ddb531",
                            CreatedAt = DateTime.Parse("2020-06-16T19:16:29.743+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Haferflocken",
                            Unit = (ProductUnit) 4,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "1eba90ad-09db-4193-9e43-f7864e07a3f0",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0780838+02:00"),
                            },
                            Id = "566b7e2e-5783-41b7-a8b6-5b10d0283aeb",
                            CreatedAt = DateTime.Parse("2020-06-16T19:18:56.102+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Nutella",
                            Unit = (ProductUnit) 8,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "8e2ed4a2-ce13-46a6-8f94-d089be00de64",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0780941+02:00"),
                            },
                            Id = "748504f7-e0ad-4cb7-8b28-93d57131038e",
                            CreatedAt = DateTime.Parse("2020-06-16T19:19:14.036+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Reis",
                            Unit = (ProductUnit) 4,
                            CategoryId = "55f92dfd-b3a8-435c-a668-ef5235174372",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "1ea9d83e-1a17-4411-b7dc-7cd3f70f2015",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781039+02:00"),
                            },
                            Id = "a5328c0d-455e-4b39-92b2-c03b4b10ff09",
                            CreatedAt = DateTime.Parse("2020-06-16T19:19:37.434+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Knoppers",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "0e43c7ea-3290-49bc-9e6c-34e2d1d0cd69",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781166+02:00"),
                            },
                            Id = "cace6184-3b1b-4729-a860-fa159f9a6706",
                            CreatedAt = DateTime.Parse("2020-06-16T19:22:02.356+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kinder Country",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "4fc39ea2-1be1-4f00-8ee3-49ecc7dd01c7",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.078127+02:00"),
                            },
                            Id = "fe9841fc-923e-405c-9d57-cb49083cb24c",
                            CreatedAt = DateTime.Parse("2020-06-16T19:23:15.875+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Oreo",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "a68c246d-4d12-4d6c-bcf0-cef23b9e330f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781367+02:00"),
                            },
                            Id = "b6bdf8bb-e678-409e-8e92-1b460c042bcc",
                            CreatedAt = DateTime.Parse("2020-06-16T19:24:40.316+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Amicelli",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "16aa42ae-8262-4fbe-80c3-6e4b7be70f48",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781461+02:00"),
                            },
                            Id = "b0bfa4c9-19ef-4572-ba58-921487b53e44",
                            CreatedAt = DateTime.Parse("2020-06-16T19:24:54.376+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Malteser",
                            Unit = (ProductUnit) 4,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "d83244de-c4ae-44e9-9a1d-38b0a0ee2e7c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781555+02:00"),
                            },
                            Id = "10993bae-5762-4a27-90fb-0f539e4fcdce",
                            CreatedAt = DateTime.Parse("2020-06-16T19:25:19.698+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Essig",
                            Unit = (ProductUnit) 6,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "5fd765c6-409b-4e7e-a141-352e6f41d673",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781654+02:00"),
                            },
                            Id = "c98c223f-974e-4cb6-a432-d8e981c738cc",
                            CreatedAt = DateTime.Parse("2020-06-16T19:26:35.5+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Zucker",
                            Unit = (ProductUnit) 2,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "bf83cd9c-7b75-4f99-9b9c-f91ada741988",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781828+02:00"),
                            },
                            Id = "1d8fafb9-9d68-4594-87e0-8be2ef16b6ce",
                            CreatedAt = DateTime.Parse("2020-06-16T19:27:57.964+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Hefe",
                            Unit = (ProductUnit) 1,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "b1600a59-56d5-4a68-8b02-5e65d9e1a630",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0781925+02:00"),
                            },
                            Id = "751bbe28-0d16-4a77-94df-d860c3dbfbe3",
                            CreatedAt = DateTime.Parse("2020-06-16T19:28:33.478+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Toilettenpapier",
                            Unit = (ProductUnit) 4,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "00df58a8-34c9-499f-863a-772298e4cefd",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0782019+02:00"),
                            },
                            Id = "7386da64-fa12-4849-8124-3b038935b565",
                            CreatedAt = DateTime.Parse("2020-06-16T19:28:58.637+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Zewa",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "4ec91ed6-dbbc-4fda-90f7-0046ad539e31",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0782121+02:00"),
                            },
                            Id = "b0f1f664-76b3-4bb0-9c1a-7235eaa1f186",
                            CreatedAt = DateTime.Parse("2020-06-16T19:32:30.644+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Febreze Lenor",
                            Unit = (ProductUnit) 9,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "dc379ea6-c755-4237-8ab6-4d411de52488",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0782214+02:00"),
                            },
                            Id = "bd0246a4-6040-4fb7-b492-f1dea92044d6",
                            CreatedAt = DateTime.Parse("2020-06-16T19:32:43.621+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Weichspüler Lenor",
                            Unit = (ProductUnit) 6,
                            CategoryId = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "d57ed4a0-fcfd-45aa-a3b4-155317a1d9b0",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0782329+02:00"),
                            },
                            Id = "6036bc4d-a8d8-48f6-8e4e-801069606641",
                            CreatedAt = DateTime.Parse("2020-06-16T19:33:26.703+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Waschmittel",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "79ef010c-1827-4808-b86a-82360976cebb",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0782455+02:00"),
                            },
                            Id = "c13bc10b-cf80-4304-81d7-fc124d3cf5d1",
                            CreatedAt = DateTime.Parse("2020-06-16T19:34:01.493+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Biomüllbeutel",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "c0a56f9d-d0e5-4a85-b5c2-682f0d9b0924",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784114+02:00"),
                            },
                            Id = "9fdaba82-2903-47e6-ad17-0f402f2ac904",
                            CreatedAt = DateTime.Parse("2020-06-16T19:35:24.662+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Backpapier",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "7f459226-3488-42d9-a636-97ac048051a4",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784245+02:00"),
                            },
                            Id = "b5b2ebbe-985c-4fa5-bc64-e2a9ff4da074",
                            CreatedAt = DateTime.Parse("2020-06-16T19:35:42.087+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Power wc",
                            Unit = (ProductUnit) 4,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "1022ae6a-25d3-4702-b76f-1a487a51ce40",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784339+02:00"),
                            },
                            Id = "4b399d0b-734b-4eda-bca3-85958c82ccb7",
                            CreatedAt = DateTime.Parse("2020-06-16T19:36:10.788+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Somat tabs",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "18f79e19-b2f7-4177-9f13-b64bdfefed7a",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784494+02:00"),
                            },
                            Id = "47df7587-aac4-4ac3-a058-d5ed651ea83e",
                            CreatedAt = DateTime.Parse("2020-06-16T19:36:34.302+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Salz spüli",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "5252300c-ad8d-456b-beac-b1662dc7b83d",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784618+02:00"),
                            },
                            Id = "0c981106-a06c-4d93-af6d-6ec6b36c385a",
                            CreatedAt = DateTime.Parse("2020-06-16T19:37:06.976+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kiwi",
                            Unit = (ProductUnit) 3,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "320fbff0-53c9-4fba-bfef-4515e4d4a174",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784714+02:00"),
                            },
                            Id = "a0fb9a9b-6915-411f-acf6-1b507985d746",
                            CreatedAt = DateTime.Parse("2020-06-16T19:39:02.213+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Piadina",
                            Unit = (ProductUnit) 4,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "7e7abc2a-a2fc-44af-b34d-5be2c207308e",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784807+02:00"),
                            },
                            Id = "6f0536b4-0049-4f72-888f-61ac3ba6bdca",
                            CreatedAt = DateTime.Parse("2020-06-16T19:40:41.389+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Glasreiniger",
                            Unit = (ProductUnit) 9,
                            CategoryId = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                            Category = new ProductCategory 
                            {
                                Name = null,
                                Id = "dbacbf78-0ea7-4c96-ae25-7fb3be0f7edb",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784901+02:00"),
                            },
                            Id = "9541a701-6eea-4925-9237-f475344a7bd6",
                            CreatedAt = DateTime.Parse("2020-06-16T19:41:01.858+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Wc Reinigung",
                            Unit = (ProductUnit) 9,
                            CategoryId = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "1c526ef6-4fcd-4bc6-a411-3948c5d53bb7",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0784996+02:00"),
                            },
                            Id = "6c769d9f-d287-430b-a966-0b6031a5d6ec",
                            CreatedAt = DateTime.Parse("2020-06-16T19:41:55.886+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Soja Joghurt",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "63d11c55-67bb-449e-8de8-f43c946e7c0f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0785116+02:00"),
                            },
                            Id = "9d1b227d-edfc-4693-b256-f44aca94948b",
                            CreatedAt = DateTime.Parse("2020-06-17T09:31:59.161+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Schmand",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "f4f9adc3-b4dc-4f1a-8c79-6c7a4e9dc4da",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.078583+02:00"),
                            },
                            Id = "35919575-928d-4101-a911-c15f0416cd8c",
                            CreatedAt = DateTime.Parse("2020-06-21T18:11:16.376+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Gin",
                            Unit = (ProductUnit) 6,
                            CategoryId = "80322c66-7733-47af-9045-2b8650993490",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "ed7b095f-dece-4a13-b9e8-048bc33ab62a",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0785932+02:00"),
                            },
                            Id = "34afe4b3-0b5c-4cb6-8840-764eebc0988e",
                            CreatedAt = DateTime.Parse("2020-06-21T18:16:08.647+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Jack Daniel",
                            Unit = (ProductUnit) 6,
                            CategoryId = "80322c66-7733-47af-9045-2b8650993490",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "f9f5d925-9503-45aa-ba86-361ff3b5d174",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786027+02:00"),
                            },
                            Id = "d168eb5b-48cb-41d2-988c-6b3245459983",
                            CreatedAt = DateTime.Parse("2020-06-21T18:17:12.221+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Speck",
                            Unit = (ProductUnit) 4,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "dfb123b0-8c13-4272-92bc-881bb38d470c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786156+02:00"),
                            },
                            Id = "856f5e1f-f66c-4a4a-b0e2-3b26fcacc75b",
                            CreatedAt = DateTime.Parse("2020-06-21T18:17:33.192+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Gurke",
                            Unit = (ProductUnit) 3,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "493699a9-569d-45a4-82bf-2b8b5fb52c5f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.078625+02:00"),
                            },
                            Id = "a88b8bfc-7245-486e-92ee-81e274a37251",
                            CreatedAt = DateTime.Parse("2020-06-21T18:18:04.846+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Spätzle",
                            Unit = (ProductUnit) 1,
                            CategoryId = "55f92dfd-b3a8-435c-a668-ef5235174372",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "884b6b2f-2980-4fcb-bd56-25469e827e5b",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786364+02:00"),
                            },
                            Id = "5594106f-ac50-41c0-ae45-ccfdc7c57f60",
                            CreatedAt = DateTime.Parse("2020-06-21T18:22:12.378+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Mayonnaise",
                            Unit = (ProductUnit) 8,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "20a12505-82ac-4a44-aaa5-3f6e1c30ca62",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786458+02:00"),
                            },
                            Id = "3fdd2229-be18-45cd-af58-5e82bd60e9a4",
                            CreatedAt = DateTime.Parse("2020-06-21T18:25:06.921+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "BBQ",
                            Unit = (ProductUnit) 9,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "09448d3d-48d4-4dc7-93df-ab2064f677bb",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786552+02:00"),
                            },
                            Id = "0b90e69b-61df-4f80-88f3-70ffd4a225e9",
                            CreatedAt = DateTime.Parse("2020-06-21T18:28:37.944+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Burgersauce",
                            Unit = (ProductUnit) 9,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c2cd41d9-bd18-4b38-a91a-e3abf09cd48c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786646+02:00"),
                            },
                            Id = "efe70426-5078-4bb7-b4a3-74ee03d5bff9",
                            CreatedAt = DateTime.Parse("2020-06-21T18:28:57.399+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Senfsoße",
                            Unit = (ProductUnit) 8,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "6970a0d1-32a4-44ef-a6a1-83dea2e21435",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786785+02:00"),
                            },
                            Id = "9795ef7e-7222-4161-8118-8c34c3166130",
                            CreatedAt = DateTime.Parse("2020-06-21T18:29:18.66+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Butter",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6134c1cf-62dc-4502-9ac6-14941c2b98ff",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "08a7b9f7-45f8-4f84-8603-c40a5dc18186",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.078688+02:00"),
                            },
                            Id = "e98e7972-c0c4-46b3-80d7-322dc1c23a55",
                            CreatedAt = DateTime.Parse("2020-06-21T18:29:59.446+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Salat",
                            Unit = (ProductUnit) 3,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "f18ca4b6-936e-489a-a4b7-853a6d80495e",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0786975+02:00"),
                            },
                            Id = "69125e19-fad4-40b9-b25a-537884412cc0",
                            CreatedAt = DateTime.Parse("2020-06-21T18:30:31.158+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Lauch",
                            Unit = (ProductUnit) 3,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "b708912a-c1fd-428c-b3a1-db8b4c375d98",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787067+02:00"),
                            },
                            Id = "7ca5b244-3bb2-476b-b48e-97d2cccd1050",
                            CreatedAt = DateTime.Parse("2020-06-21T18:30:59.873+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Frühligszwiebeln",
                            Unit = (ProductUnit) 3,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "1ec62222-1fc2-4210-8828-efe0f5662d88",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787183+02:00"),
                            },
                            Id = "ebf60dbc-a927-448b-ae72-6b6cfeead11f",
                            CreatedAt = DateTime.Parse("2020-06-21T18:32:21.983+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Rote Zwiebeln",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "1971f249-2277-44a1-9565-9b818c2cca07",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787277+02:00"),
                            },
                            Id = "bf138a39-4d99-4641-a998-4b106a435c1c",
                            CreatedAt = DateTime.Parse("2020-06-21T18:34:24.067+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kirschen",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "d5840409-a846-4b7c-aabd-597df7febc38",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787403+02:00"),
                            },
                            Id = "f2f32fff-54cc-4a51-831b-c3bc7f9ceadd",
                            CreatedAt = DateTime.Parse("2020-06-21T18:35:02.931+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Pfirsich",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "82c17133-704a-43b4-9370-3d4926bb46e2",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787496+02:00"),
                            },
                            Id = "c3e809de-9374-49dc-af19-d5bbde0ad757",
                            CreatedAt = DateTime.Parse("2020-06-21T18:36:22.873+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Zitrone",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "772d2ebf-d8fe-49ed-b51c-57b139754464",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787588+02:00"),
                            },
                            Id = "8d619428-8edf-4fbd-ab22-80abc19e9c16",
                            CreatedAt = DateTime.Parse("2020-06-21T18:36:41.887+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Limetten",
                            Unit = (ProductUnit) 1,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "842f05bf-28a8-46c0-8512-a2d8c49df02a",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787679+02:00"),
                            },
                            Id = "b9ba9840-e02a-4426-9870-c3656a14123e",
                            CreatedAt = DateTime.Parse("2020-06-21T18:39:36.007+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Croissants Teig",
                            Unit = (ProductUnit) 3,
                            CategoryId = "4a1084ab-82d1-4838-95ef-710c811e6b06",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "d6ca8615-20ec-43b8-8f97-bfe36fd4c779",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787774+02:00"),
                            },
                            Id = "d41765c2-c67c-4abc-9ee7-6a60ce2ef922",
                            CreatedAt = DateTime.Parse("2020-06-21T18:39:50.054+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Paprika Pulver",
                            Unit = (ProductUnit) 8,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "3b7ab780-0541-4956-85ee-28e4f0926ee2",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787866+02:00"),
                            },
                            Id = "35541cbc-f86b-440d-abd8-4a86b7490e1f",
                            CreatedAt = DateTime.Parse("2020-06-21T18:43:46.961+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Pfeffer",
                            Unit = (ProductUnit) 8,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "f704fb6c-312a-4c0b-b007-50843f15af44",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0787986+02:00"),
                            },
                            Id = "0f33b2f4-f34c-4c89-8454-8d2ecfaf26ee",
                            CreatedAt = DateTime.Parse("2020-06-21T18:45:24.417+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Salz",
                            Unit = (ProductUnit) 8,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "82aa7747-6cb8-4fad-bb51-2486ca5dc143",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788077+02:00"),
                            },
                            Id = "27ba1762-6f3a-429c-8d18-9b9aa76b1459",
                            CreatedAt = DateTime.Parse("2020-06-21T18:46:00.486+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Maggi",
                            Unit = (ProductUnit) 4,
                            CategoryId = "2a8f6252-cfe5-4f3b-80cb-b12707152c23",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "9340c037-1a60-47bf-a49d-1533247c610d",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788171+02:00"),
                            },
                            Id = "daebddad-e40c-44b4-ad77-7c7822b8de92",
                            CreatedAt = DateTime.Parse("2020-06-21T18:46:22.242+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Schlemmerfilet",
                            Unit = (ProductUnit) 4,
                            CategoryId = "42b96200-8d14-4d43-9ef4-02cc372c3e08",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "29a4b98c-3da5-4d47-ae20-b5ff2d17f224",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788376+02:00"),
                            },
                            Id = "93ed9751-531b-4035-b99d-d8b826c63111",
                            CreatedAt = DateTime.Parse("2020-06-21T18:57:20.135+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Fischstäbchen",
                            Unit = (ProductUnit) 4,
                            CategoryId = "42b96200-8d14-4d43-9ef4-02cc372c3e08",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "43543efd-7200-45ab-91b7-f04b99df42fc",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788531+02:00"),
                            },
                            Id = "40b85fd5-8138-4a60-be41-3faa9f7b28f2",
                            CreatedAt = DateTime.Parse("2020-06-21T18:58:02.115+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Waldfrüchte",
                            Unit = (ProductUnit) 4,
                            CategoryId = "42b96200-8d14-4d43-9ef4-02cc372c3e08",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "d943f67c-6660-4ec2-931a-59e970e6ec3c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788655+02:00"),
                            },
                            Id = "29561781-1166-4d8d-9500-9ecaea612bf5",
                            CreatedAt = DateTime.Parse("2020-06-21T18:59:00.222+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Sojasoße",
                            Unit = (ProductUnit) 9,
                            CategoryId = "1e190d8a-f989-4b85-a61d-38b054c1c15d",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "2686cb30-b19a-41b2-99eb-38a5711943bc",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788772+02:00"),
                            },
                            Id = "eca6fc80-b3ba-4ef0-ba16-db0e12802536",
                            CreatedAt = DateTime.Parse("2020-06-21T19:02:25.188+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Röstzwiebeln",
                            Unit = (ProductUnit) 7,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "3abfc9c0-f94d-4407-b0b0-28e426ad445c",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0788888+02:00"),
                            },
                            Id = "b9cc2872-edd9-4be6-8dba-01204895c064",
                            CreatedAt = DateTime.Parse("2020-06-21T19:02:46.464+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Danklorix candegina",
                            Unit = (ProductUnit) 9,
                            CategoryId = "2f8d0fed-725d-47f0-b353-22f39a2ac5e8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c6917270-2b84-4a5b-b9dd-6e053122bbd1",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0789028+02:00"),
                            },
                            Id = "dc0009da-e4fb-4e99-9232-ef7e39199bcc",
                            CreatedAt = DateTime.Parse("2020-06-21T19:04:10.003+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Feuchttücher",
                            Unit = (ProductUnit) 4,
                            CategoryId = "7eb72a7e-bf70-4d88-a765-9ad73bb35a2c",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "bcefa5a2-fc53-4a94-9b7d-f1afcb2d93b5",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0789145+02:00"),
                            },
                            Id = "4e62550f-c0ce-4539-aa37-47eaf7d3fbe9",
                            CreatedAt = DateTime.Parse("2020-06-21T19:07:28.673+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Popcorn",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6a1c39dd-7a45-4bb5-ae0e-ba0aa99d139f",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "dc96dca3-b47c-4659-aa65-5c89e14d7a79",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0789241+02:00"),
                            },
                            Id = "958cbfde-338b-47e8-a164-928936aa4371",
                            CreatedAt = DateTime.Parse("2020-06-21T19:09:16.589+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Rosinen",
                            Unit = (ProductUnit) 4,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c6e9b1e2-ece7-4815-bd61-c407b00aab10",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0789341+02:00"),
                            },
                            Id = "1613cc33-a94b-4ec7-93b1-0be9e7cf4d5c",
                            CreatedAt = DateTime.Parse("2020-06-21T19:11:02.713+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Mehl",
                            Unit = (ProductUnit) 2,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "c8feb064-ac9f-4521-a535-2e884d09191a",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0789521+02:00"),
                            },
                            Id = "430e2c7d-dd26-4051-a212-4a0406084550",
                            CreatedAt = DateTime.Parse("2020-06-21T19:11:38.554+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Vollkorn Mehl",
                            Unit = (ProductUnit) 2,
                            CategoryId = "bab91259-2b36-4f62-871c-857f8e2bef95",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "4666f067-8b07-4d98-9cc5-1052e06a0400",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0789654+02:00"),
                            },
                            Id = "36494252-dea3-4a8e-93b8-bf90973b4050",
                            CreatedAt = DateTime.Parse("2020-06-21T19:15:12.384+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Ingwer",
                            Unit = (ProductUnit) 1,
                            CategoryId = "9048b819-c949-4fb3-843b-372c6900b336",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "4aa7f95c-4028-43cd-bd64-a49e59207217",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.079078+02:00"),
                            },
                            Id = "c800e190-4d94-4f07-a75a-734f49923492",
                            CreatedAt = DateTime.Parse("2020-06-22T19:22:53.922+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Minze",
                            Unit = (ProductUnit) 1,
                            CategoryId = "9048b819-c949-4fb3-843b-372c6900b336",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "1e530e56-7874-4506-b849-35036f2413f1",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0791015+02:00"),
                            },
                            Id = "f87a7567-0634-46a0-ac2c-411cbf13f9d3",
                            CreatedAt = DateTime.Parse("2020-06-22T19:24:45.17+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Weisse Schokolade",
                            Unit = (ProductUnit) 1,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "b6076057-e0cd-42f1-b8bc-f781ef85622e",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.079111+02:00"),
                            },
                            Id = "dbbefa85-db42-4a0b-bb89-63278ba2a03f",
                            CreatedAt = DateTime.Parse("2020-06-25T18:14:00.899+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Kokosraspeln",
                            Unit = (ProductUnit) 1,
                            CategoryId = "23736a14-2e67-45f8-9da8-0b15ba31a348",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "cbeed5f5-f8ac-4b14-a0e2-c4352f171d7f",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0791206+02:00"),
                            },
                            Id = "f348c1b7-ed57-434e-b4a5-6cd1c0314615",
                            CreatedAt = DateTime.Parse("2020-06-25T18:14:32.539+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Mandeln",
                            Unit = (ProductUnit) 1,
                            CategoryId = "543a909f-f689-4046-a44e-c3e1bb8f9c09",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "9f8fb54d-991a-4f12-85d2-d6cfa5e17b6d",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0791424+02:00"),
                            },
                            Id = "f414655e-9eb1-4370-bbd4-3b6fe927a9fa",
                            CreatedAt = DateTime.Parse("2020-06-25T18:16:05.174+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Melone",
                            Unit = (ProductUnit) 3,
                            CategoryId = "6e04fc0f-9f78-4a31-91aa-cb4c838867c0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "3ed5b993-09c1-41e6-8b15-de09b551eaf3",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0792647+02:00"),
                            },
                            Id = "b7b74880-6f8e-4635-b9ae-3c54fc0b4591",
                            CreatedAt = DateTime.Parse("2020-06-25T18:25:26.904+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Rucola",
                            Unit = (ProductUnit) 1,
                            CategoryId = "8b768402-fd38-4605-8b4a-1b6519b9a4c8",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "ed53b8b4-ff39-41ec-b7f0-c199a02730d9",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0792748+02:00"),
                            },
                            Id = "5ac75b9f-d7e8-433e-b6aa-cb9f262e1d58",
                            CreatedAt = DateTime.Parse("2020-06-26T17:37:49.77+02:00"),
                        },
                        new ProductItem
                        {
                            Name = "Parmaschinken",
                            Unit = (ProductUnit) 1,
                            CategoryId = "d22dd97b-52e8-4bab-bb0c-cf6c53b73ee0",
                            Category = new ProductCategory
                            {
                                Name = null,
                                Id = "9cb425f6-4ac1-472b-8dfb-e5ad1c175c72",
                                CreatedAt = DateTime.Parse("2020-07-03T12:34:43.0792847+02:00"),
                            },
                            Id = "ea2b7b72-a82d-4703-b4f9-5295f8eb1f51",
                            CreatedAt = DateTime.Parse("2020-06-26T17:38:27.061+02:00"),
                        }
                    },
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            Name = "Philipp\u0026Zaira",
                            OwnerId = "9f7b66b5-32b8-4ccd-8ec9-b64c7e95b33d",
                            ShoppingLists = new List<ShoppingList>(),
                            Members = new List<ShoppingUserModel>
                            {
                                new ShoppingUserModel
                                {
                                    Id = "9f7b66b5-32b8-4ccd-8ec9-b64c7e95b33d",
                                    UserName = null,
                                    Email = null
                                },
                                new ShoppingUserModel
                                {
                                    Id = "1e60701d-0af3-40f2-b873-6eb0bc888912",
                                    UserName = null,
                                    Email = null
                                }
                            },
                            Id = "8afcfa3b-895b-4333-85cd-b97cd6bb4a73",
                            CreatedAt = DateTime.Parse("2020-06-11T23:50:38.779+02:00"),
                        }
                    },
                    ShoppingLists = new List<ShoppingList>
                    {
                        new ShoppingList
                        {
                            Name = "List for Week 25",
                            ListDate = DateTime.Parse( "2020-06-17T00:00:00"),
                            Owner = new ShoppingUserModel
                            {
                                Id = "9f7b66b5-32b8-4ccd-8ec9-b64c7e95b33d",
                                UserName =  null,
                                Email =  null
                            },
                            UserGroups = new List<UserGroup>(),
                            Items = new List<ShoppingListItem>
                            {
                                new ShoppingListItem
                                {
                                    ProductItemId = "eb5d7933-c875-4333-8360-c9140ed050e1",
                                    ProductItem = null,
                                    Amount = 1500,
                                    Done = true,
                                    Id = "4f0cb7ba-2557-4449-a792-06cc5e6869d7",
                                    CreatedAt = DateTime.Parse("2020-06-16T18:07:05.658+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "23213305-ebd3-45ee-aec4-2c483c34c2a4",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "1bc002f5-f8a8-479e-9fe9-f6c4c21a82b3",
                                    CreatedAt = DateTime.Parse("2020-06-16T18:08:14.413+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "677aa76e-3160-4484-9fb8-d06ab9171bc2",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "0fe9133c-903a-46ed-801e-d177e0a66637",
                                    CreatedAt = DateTime.Parse("2020-06-16T18:09:07.956+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "9b60f954-1eaa-4e4e-acb4-da1bbc9bfb56",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "6b16c95a-4b7a-4ee9-b007-f167f027704e",
                                    CreatedAt = DateTime.Parse("2020-06-16T18:09:45.182+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "633fcf92-65f3-47d5-90db-c353286d2122",
                                    ProductItem = null,
                                    Amount = 2,
                                    Done = true,
                                    Id = "d64c7b58-c220-48ca-8b5a-348cbc0fe31c",
                                    CreatedAt = DateTime.Parse("2020-06-16T18:54:18.326+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "e480d797-5c16-4269-8868-ed94586c67b9",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "8e70a719-91e1-467a-9fb9-0c7478aaede8",
                                    CreatedAt = DateTime.Parse("2020-06-16T19:44:49.755+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "8ab8794f-9f73-465e-89af-3ce1361bbfd4",
                                    ProductItem = null,
                                    Amount = 500,
                                    Done = true,
                                    Id = "b5618978-e0ac-441c-aec4-3a9ec8115d31",
                                    CreatedAt = DateTime.Parse("2020-06-16T19:46:40.03+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "0e6cc8fd-2436-4881-a864-eb2cd6b52b2c",
                                    ProductItem = null,
                                    Amount = 3,
                                    Done = true,
                                    Id = "d891ca6c-24c7-444f-a8f8-dfa6e9411de4",
                                    CreatedAt = DateTime.Parse("2020-06-16T19:47:31.623+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "089f24cc-9110-4df3-a1e7-8831776fdd83",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "0044105e-59d0-4240-9069-eb2e690654f6",
                                    CreatedAt = DateTime.Parse("2020-06-16T19:47:41.891+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "7d0660cd-b813-48e6-b553-648048fb0034",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "52951801-8e64-4b50-abf5-51f315cf1a73",
                                    CreatedAt = DateTime.Parse("2020-06-16T19:45:03.595+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "9d1b227d-edfc-4693-b256-f44aca94948b",
                                    ProductItem = null,
                                    Amount = 2,
                                    Done = true,
                                    Id = "1247b93e-5be7-48c0-8f12-111e2e9dcf4f",
                                    CreatedAt = DateTime.Parse("2020-06-17T09:32:20.592+02:00"),
                                }
                            },
                            Id = "3fae3463-207c-45b3-8ccc-c2223eda38ad",
                            CreatedAt = DateTime.Parse("2020-06-16T18:05:51.768+02:00"),
                        },
                        new ShoppingList
                        {
                            Name = "Sa-So",
                            ListDate = DateTime.Parse( "2020-06-20T14:49:11.182+02:00"),
                            Owner = new ShoppingUserModel
                            {
                                Id = "1e60701d-0af3-40f2-b873-6eb0bc888912",
                                UserName =  null,
                                Email =  null
                            },
                            UserGroups = new List<UserGroup>(),
                            Items = new List<ShoppingListItem>
                            {
                                new ShoppingListItem
                                {
                                    ProductItemId = "5f1e3d16-cae8-48c5-96b3-347ff36f3766",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "5f2e4975-5f8d-48ae-b0c7-0175ff505f54",
                                    CreatedAt = DateTime.Parse("2020-06-20T14:49:29.501+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "eb5d7933-c875-4333-8360-c9140ed050e1",
                                    ProductItem = null,
                                    Amount = 500,
                                    Done = true,
                                    Id = "3e4532f8-430a-42eb-8793-0e22c67f1ad0",
                                    CreatedAt = DateTime.Parse("2020-06-20T14:50:07.984+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "e22bbc71-0efc-48fd-9e71-50405afc3e9b",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "92c99b96-6157-418c-97f6-e4e5e829f87f",
                                    CreatedAt = DateTime.Parse("2020-06-20T14:50:33.468+02:00"),
                                }
                            },
                            Id = "eacea64d-7f56-4009-9490-0664819e5736",
                            CreatedAt = DateTime.Parse("2020-06-20T14:49:11.182+02:00"),
                        },
                        new ShoppingList
                        {
                            Name = "List2206",
                            ListDate = DateTime.Parse( "2020-06-22T19:20:45.683+02:00"),
                            Owner = new ShoppingUserModel
                            {
                                Id = "9f7b66b5-32b8-4ccd-8ec9-b64c7e95b33d",
                                UserName =  null,
                                Email =  null
                            },
                            UserGroups = new List<UserGroup>(),
                            Items = new List<ShoppingListItem>
                            {
                                new ShoppingListItem
                                {
                                    ProductItemId = "b9cc2872-edd9-4be6-8dba-01204895c064",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "d25e74a4-2393-48e3-8347-6ef43420deeb",
                                    CreatedAt = DateTime.Parse("2020-06-22T19:21:12.068+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "8d619428-8edf-4fbd-ab22-80abc19e9c16",
                                    ProductItem = null,
                                    Amount = 500,
                                    Done = true,
                                    Id = "3c1e38f7-f2a6-4704-a316-170d72e0e563",
                                    CreatedAt = DateTime.Parse("2020-06-22T19:21:24.576+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "c800e190-4d94-4f07-a75a-734f49923492",
                                    ProductItem = null,
                                    Amount = 50,
                                    Done = true,
                                    Id = "a4cd4ba7-17b4-454a-bb95-a955ba61e6aa",
                                    CreatedAt = DateTime.Parse("2020-06-22T19:26:26.524+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "f87a7567-0634-46a0-ac2c-411cbf13f9d3",
                                    ProductItem = null,
                                    Amount = 50,
                                    Done = false,
                                    Id = "36dbb18f-3982-4be0-9899-895d73927a17",
                                    CreatedAt = DateTime.Parse("2020-06-22T19:27:04.626+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "23213305-ebd3-45ee-aec4-2c483c34c2a4",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "6d0650c4-537d-4d53-ac5c-5cd0f47daf2a",
                                    CreatedAt = DateTime.Parse("2020-06-23T12:15:09.832+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "677aa76e-3160-4484-9fb8-d06ab9171bc2",
                                    ProductItem = null,
                                    Amount = 2,
                                    Done = true,
                                    Id = "fd6c2483-67d9-40fa-9da2-cc53fb82f804",
                                    CreatedAt = DateTime.Parse("2020-06-23T12:15:33.881+02:00"),
                                }
                            },
                            Id = "f150a4c9-b09d-4e20-81de-f7312df632f5",
                            CreatedAt = DateTime.Parse("2020-06-22T19:20:45.683+02:00"),
                        },
                        new ShoppingList
                        {
                            Name = "List of 25.06.20",
                            ListDate = DateTime.Parse( "2020-06-25T18:16:47.816+02:00"),
                            Owner = new ShoppingUserModel
                            {
                                Id = "9f7b66b5-32b8-4ccd-8ec9-b64c7e95b33d",
                                UserName =  null,
                                Email =  null
                            },
                            UserGroups = new List<UserGroup>(),
                            Items = new List<ShoppingListItem>
                            {
                                new ShoppingListItem
                                {
                                    ProductItemId = "dbbefa85-db42-4a0b-bb89-63278ba2a03f",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "7900d033-b8fa-455b-82f3-fd8a3716e224",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:16:52.35+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "f414655e-9eb1-4370-bbd4-3b6fe927a9fa",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "ee0bf09c-4909-491a-9352-e71c856a947d",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:17:48.143+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "f348c1b7-ed57-434e-b4a5-6cd1c0314615",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "3265cfe7-7e2a-4f5a-bd93-ddca9de8c5c0",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:18:19.8+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "7d0660cd-b813-48e6-b553-648048fb0034",
                                    ProductItem = null,
                                    Amount = 5,
                                    Done = true,
                                    Id = "d7b3dce7-129e-456a-b16a-9dbff3971e0d",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:18:57.74+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "8ab8794f-9f73-465e-89af-3ce1361bbfd4",
                                    ProductItem = null,
                                    Amount = 100,
                                    Done = true,
                                    Id = "2cc41030-ac15-4846-8067-1bbb291783fd",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:20:27.469+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "fb5179b3-1192-47f3-a731-4ae1d335fce6",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "5fab8bbb-b11e-4a9e-bc1b-c1ba57479de6",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:20:38.471+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "b7b74880-6f8e-4635-b9ae-3c54fc0b4591",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "ddba3fe0-74b3-4d07-bd37-fd27b6d11851",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:27:00.183+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "fd421372-99e8-4359-953b-240e6a82843d",
                                    ProductItem = null,
                                    Amount = 100,
                                    Done = false,
                                    Id = "1297b121-f734-4e39-b165-0a0ab2bc8537",
                                    CreatedAt = DateTime.Parse("2020-06-25T18:27:16.774+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "089f24cc-9110-4df3-a1e7-8831776fdd83",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "7efd9a50-da98-44de-bbfb-e5b6149fd2a1",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:36:48.088+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "856f5e1f-f66c-4a4a-b0e2-3b26fcacc75b",
                                    ProductItem = null,
                                    Amount = 4,
                                    Done = true,
                                    Id = "66a7ab0d-b05d-4390-a0b2-89a77d0e5da5",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:36:53.801+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "ea2b7b72-a82d-4703-b4f9-5295f8eb1f51",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "b143f811-069a-4a3a-aa44-2468c4e9a23b",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:39:06.008+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "5938480d-24c0-4d84-a695-201db56a543f",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "b889090a-4922-45ec-b0bb-90dae38f5bc0",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:39:20.744+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "e480d797-5c16-4269-8868-ed94586c67b9",
                                    ProductItem = null,
                                    Amount = 0.5f,
                                    Done = true,
                                    Id = "1c8afab3-f759-4914-a07a-2b2edae46fbe",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:40:20.127+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "9e4512ee-f25b-4dea-bae5-8bbbb253ca5a",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "d34dd972-1542-4da0-aac3-0e051e0dd3c0",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:40:46.22+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "73285021-f3db-451d-a1a0-13395fc9ae70",
                                    ProductItem = null,
                                    Amount = 2,
                                    Done = true,
                                    Id = "6b8baff2-9962-4860-81ad-1c019a6c9efd",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:40:51.981+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "a88b8bfc-7245-486e-92ee-81e274a37251",
                                    ProductItem = null,
                                    Amount = 1,
                                    Done = true,
                                    Id = "ae8e9178-d8a4-47f8-8c04-b2d69287a491",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:41:06.832+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "5ac75b9f-d7e8-433e-b6aa-cb9f262e1d58",
                                    ProductItem = null,
                                    Amount = 200,
                                    Done = true,
                                    Id = "e20f24cc-dc29-4922-a5d3-c621a2774f18",
                                    CreatedAt = DateTime.Parse("2020-06-26T17:57:17.785+02:00"),
                                }
                            },
                            Id = "d62bca8f-2215-4c34-8c92-10232c5d9eb3",
                            CreatedAt = DateTime.Parse("2020-06-25T18:16:47.815+02:00"),
                        },
                        new ShoppingList
                        {
                            Name = "List of 01.07.20",
                            ListDate = DateTime.Parse( "2020-07-01T08:08:14.658+02:00"),
                            Owner = new ShoppingUserModel
                            {
                                Id = "9f7b66b5-32b8-4ccd-8ec9-b64c7e95b33d",
                                UserName =  null,
                                Email =  null
                            },
                            UserGroups = new List<UserGroup>(),
                            Items = new List<ShoppingListItem>
                            {
                                new ShoppingListItem
                                {
                                    ProductItemId = "7d0660cd-b813-48e6-b553-648048fb0034",
                                    ProductItem = null,
                                    Amount = 4,
                                    Done = false,
                                    Id = "bf25fded-c40e-461e-bc6a-773211b79a88",
                                    CreatedAt = DateTime.Parse("2020-07-01T08:08:22.923+02:00"),
                                },
                                new ShoppingListItem
                                {
                                    ProductItemId = "633fcf92-65f3-47d5-90db-c353286d2122",
                                    ProductItem = null,
                                    Amount = 2,
                                    Done = false,
                                    Id = "5049904d-764a-419b-9d76-bedc53b82bbc",
                                    CreatedAt = DateTime.Parse("2020-07-01T08:08:28.761+02:00"),
                                }
                            },
                            Id = "7b41c537-7ea0-43d1-b735-c6dbdfbd643b",
                            CreatedAt = DateTime.Parse("2020-07-01T08:08:14.658+02:00"),
                        }
                    },
                    UserGroupShoppingLists = new List<UserGroupShoppingList>
                    {
                        new UserGroupShoppingList
                        {
                            UserGroupId =  "8afcfa3b-895b-4333-85cd-b97cd6bb4a73",
                            UserGroup =  null,
                            ShoppingListId =  "3fae3463-207c-45b3-8ccc-c2223eda38ad",
                            ShoppingList =  null,
                            Id = "da97f7da-58ff-413b-8b04-563a1aec03fd",
                            CreatedAt = DateTime.Parse("2020-06-16T18:07:05.504+02:00"),
                        },
                        new UserGroupShoppingList
                        {
                            UserGroupId =  "8afcfa3b-895b-4333-85cd-b97cd6bb4a73",
                            UserGroup =  null,
                            ShoppingListId =  "eacea64d-7f56-4009-9490-0664819e5736",
                            ShoppingList =  null,
                            Id = "244cfe05-d309-4f28-bbd5-db576f55a447",
                            CreatedAt = DateTime.Parse("2020-06-20T14:49:29.191+02:00"),
                        },
                        new UserGroupShoppingList
                        {
                            UserGroupId =  "8afcfa3b-895b-4333-85cd-b97cd6bb4a73",
                            UserGroup =  null,
                            ShoppingListId =  "f150a4c9-b09d-4e20-81de-f7312df632f5",
                            ShoppingList =  null,
                            Id = "298fc153-f202-4d98-8063-2503233f495f",
                            CreatedAt = DateTime.Parse("2020-06-22T19:21:11.839+02:00"),
                        },
                        new UserGroupShoppingList
                        {
                            UserGroupId =  "8afcfa3b-895b-4333-85cd-b97cd6bb4a73",
                            UserGroup =  null,
                            ShoppingListId =  "d62bca8f-2215-4c34-8c92-10232c5d9eb3",
                            ShoppingList =  null,
                            Id = "c74ff61f-a10b-4c66-9586-92a39adbef6d",
                            CreatedAt = DateTime.Parse("2020-06-25T18:16:52.123+02:00"),
                        },
                        new UserGroupShoppingList
                        {
                            UserGroupId =  "8afcfa3b-895b-4333-85cd-b97cd6bb4a73",
                            UserGroup =  null,
                            ShoppingListId =  "7b41c537-7ea0-43d1-b735-c6dbdfbd643b",
                            ShoppingList =  null,
                            Id = "c1526bf6-5f7d-4667-b99c-1e0624090e49",
                            CreatedAt = DateTime.Parse("2020-07-01T08:08:19.913+02:00"),
                        }
                    }
                }
            };
        }
    }
}

