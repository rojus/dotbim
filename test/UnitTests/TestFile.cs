﻿using System;
using System.Collections.Generic;
using dotbim;
using Xunit;

namespace test.UnitTests
{
    public class TestFile
    {

        #region Properties

        [Fact]
        public void TestFile_Correct_ElementProperties()
        {
            File file = ToolboxForTests.CreateTestFileWithTriangleBluePlate();
            
            Assert.Equal(1, file.Elements.Count);
            var element = file.Elements[0];
            
            Assert.True(ToolboxForTests.IsColorSame(element.Color, (0, 120, 120, 255)));
            Assert.Equal("d4f28792-e1e9-4e31-bcee-740dbda61e20", element.Guid);
            Assert.Equal("Name", element.Info.Keys[0]);
            Assert.Equal("Triangle", element.Info.Values[0]);
            Assert.Equal(0, element.MeshId);
            Assert.True(ToolboxForTests.IsRotationSame(element.Rotation, (0.0, 0.0, 0.0, 1.0), 0.001));
            Assert.Equal("Plate", element.Type);
            Assert.True(ToolboxForTests.IsVectorSame(element.Vector, (0,0,0), 0.001));
        }
        
        [Fact]
        public void TestFile_Correct_MeshProperties()
        {
            File file = ToolboxForTests.CreateTestFileWithTriangleBluePlate();
            
            Assert.Equal(1, file.Meshes.Count);
            var mesh = file.Meshes[0];
            
            Assert.Equal(0, mesh.MeshId);
            
            Assert.Equal(new List<double>
            {
                0.0,0.0,0.0,
                10.0,0.0,0.0,
                10.0,-15.0,0.0,
            }, mesh.VerticesCoordinates);
            
            Assert.Equal(new List<int>
            {
                0,1,2
            }, mesh.FacesIds);
        }

        [Fact]
        public void TestFile_Correct_OtherProperties()
        {
            File file = ToolboxForTests.CreateTestFileWithTriangleBluePlate();
            
            Assert.Equal("1.0.0", file.SchemaVersion);
            Assert.Equal("Author", file.Info.Keys[0]);
            Assert.Equal("John Doe", file.Info.Values[0]);
        }

        #endregion

        #region Save&Read

        [Fact]
        public void TestFile_TrianglePlate_SaveAndRead()
        {
            string path = "TrianglePlate.bim";
            
            File file = ToolboxForTests.CreateTestFileWithTriangleBluePlate();
            file.Save(path);

            var fileRead = File.Read(path);
            
            Assert.Equal(1, fileRead.Elements.Count);
            var element = file.Elements[0];
            
            Assert.True(ToolboxForTests.IsColorSame(element.Color, (0, 120, 120, 255)));
            Assert.Equal("d4f28792-e1e9-4e31-bcee-740dbda61e20", element.Guid);
            Assert.Equal("Name", element.Info.Keys[0]);
            Assert.Equal("Triangle", element.Info.Values[0]);
            Assert.Equal(0, element.MeshId);
            Assert.True(ToolboxForTests.IsRotationSame(element.Rotation, (0.0, 0.0, 0.0, 1.0), 0.001));
            Assert.Equal("Plate", element.Type);
            Assert.True(ToolboxForTests.IsVectorSame(element.Vector, (0,0,0), 0.001));
            
            Assert.Equal(1, file.Meshes.Count);
            var mesh = file.Meshes[0];
            
            Assert.Equal(0, mesh.MeshId);
            
            Assert.Equal(new List<double>
            {
                0.0,0.0,0.0,
                10.0,0.0,0.0,
                10.0,-15.0,0.0,
            }, mesh.VerticesCoordinates);
            
            Assert.Equal(new List<int>
            {
                0,1,2
            }, mesh.FacesIds);
            
            Assert.Equal("1.0.0", file.SchemaVersion);
            Assert.Equal("Author", file.Info.Keys[0]);
            Assert.Equal("John Doe", file.Info.Values[0]);
        }

        [Fact]
        public void TestFile_WrongPathWrite_ThrowsException()
        {
            File file = new File();
            var exception = Assert.Throws<ArgumentException>(() => file.Save("Test.another"));
            Assert.Equal("Path should end up with .bim", exception.Message);
        }
        
        [Fact]
        public void TestFile_WrongPathRead_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => File.Read("Test.another"));
            Assert.Equal("Path should end up with .bim", exception.Message);
        }

        #endregion
        

    }
}