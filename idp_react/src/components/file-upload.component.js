import React, { useRef,useState, Component } from "react";
import fileUploadService from "../services/file-upload.service";
// import {
//   FileUploadContainer,
//   FormField,
//   DragDropText,
//   UploadFileBtn,
//   FilePreviewContainer,
//   ImagePreview,
//   PreviewContainer,
//   PreviewList,
//   FileMetaData,
//   RemoveFileIcon,
//   InputLabel
// } from "./file-upload.styles";

// const DEFAULT_MAX_FILE_SIZE_IN_BYTES = 50000000;
// const KILO_BYTES_PER_BYTE = 100000;


// const convertNestedObjectToArray = (nestedObj) =>Object.keys(nestedObj).map((key) => nestedObj[key]);

// const convertBytesToKB = (bytes) => Math.round(bytes / KILO_BYTES_PER_BYTE);

// const FileUpload = ({
//     label,
//   updateFilesCb,
//   maxFileSizeInBytes = DEFAULT_MAX_FILE_SIZE_IN_BYTES,
//   ...otherProps
// }) => {
//   const fileInputField = useRef(null);
//   const [files, setFiles] = useState({});

//   const handleUploadBtnClick = () => {
//     fileInputField.current.click();
//   };

//   const convertNestedObjectToArray = (nestedObj) => Object.keys(nestedObj).map((key) => nestedObj[key]);

// const callUpdateFilesCb = (files) => {
//   const filesAsArray = convertNestedObjectToArray(files);
//   updateFilesCb(filesAsArray);
// };

//   const handleNewFileUpload = (e) => {
//     const { files: newFiles } = e.target;
//     if (newFiles.length) {
//       let updatedFiles = addNewFiles(newFiles);
//       setFiles(updatedFiles);
//       callUpdateFilesCb(updatedFiles);
//     }
//   };

//   const addNewFiles = (newFiles) => {
//     for (let file of newFiles) {
//       if (file.size <= maxFileSizeInBytes) {
//         if (!otherProps.multiple) {
//           return { file };
//         }
//         files[file.name] = file;
//       }
//     }
//     return { ...files };
//   };

//   const removeFile = (fileName) => {
//     delete files[fileName];
//     setFiles({ ...files });
//     callUpdateFilesCb({ ...files });
//   };

//   return (
//     <>
//     <FileUploadContainer>
//         <InputLabel>{label}</InputLabel>
//         <DragDropText>Drag and drop your files anywhere or</DragDropText>
//         <UploadFileBtn type="button" onClick={handleUploadBtnClick}>
//           <i className="fas fa-file-upload" />
//           <span> Upload {otherProps.multiple ? "files" : "a file"}</span>
//         </UploadFileBtn>
//         <FormField
//           type="file"
//           ref={fileInputField}
//           onChange={handleNewFileUpload}
//           title=""
//           value=""
//           {...otherProps}
//         />
//       </FileUploadContainer>
//        {/*second part starts here*/}
//        <FilePreviewContainer>
//         <span>To Upload</span>
//         <PreviewList>
//           {Object.keys(files).map((fileName, index) => {
//             let file = files[fileName];
//             let isImageFile = file.type.split("/")[0] === "image";
//             return (
//               <PreviewContainer key={fileName}>
//                 <div>
//                   {isImageFile && (
//                     <ImagePreview
//                       src={URL.createObjectURL(file)}
//                       alt={`file preview ${index}`}
//                     />
//                   )}
//                   <FileMetaData isImageFile={isImageFile}>
//                     <span>{file.name}</span>
//                     <aside>
//                       <span>{convertBytesToKB(file.size)} kb</span>
//                       <RemoveFileIcon className="fas fa-trash-alt"
//                         onClick={() => removeFile(fileName)}
//                       />
//                     </aside>
//                   </FileMetaData>
//                 </div>
//               </PreviewContainer>
//             );
//           })}
//         </PreviewList>
//       </FilePreviewContainer>
//       </>
//   );
// }

// export default FileUpload;
export default class UploadFiles extends Component {
  constructor(props) {
    super(props);

    this.state = {
      selectedFiles: undefined,
      currentFile: undefined,
      progress: 0,
      message: "",

      fileInfos: [],
    };
  }

  componentDidMount() {
    fileUploadService.getFiles().then((response) => {
      this.setState({
        fileInfos: response.data,
      });
    });
  }

  selectFile=(event)=> { 
    this.setState({
      selectedFiles: event?.target?.files,
    });
  }
    upload=()=> {
      debugger
      let currentFile = this.state?.selectedFiles[0];
  
      this.setState({
        progress: 0,
        currentFile: currentFile,
      });
  
      fileUploadService.upload(currentFile, (event) => {
        this.setState({
          progress: Math.round((100 * event.loaded) / event.total),
        });
      })
        .then((response) => {
          this.setState({
            message: response.data.message,
          });
          return fileUploadService.getFiles();
        })
        .then((files) => {
          this.setState({
            fileInfos: files.data,
          });
        })
        .catch(() => {
          this.setState({
            progress: 0,
            message: "Could not upload the file!",
            currentFile: undefined,
          });
        });
  
      this.setState({
        selectedFiles: undefined,
      });
    }
  render() {

    const {
      selectedFiles,
      currentFile,
      progress,
      message,
      fileInfos,
    } = this.state;
console.log("fileInfos", fileInfos)
console.log("selectedFiles", selectedFiles)

    return (
      <div>
        {currentFile && (
          <div className="progress">
            <div
              className="progress-bar progress-bar-info progress-bar-striped"
              role="progressbar"
              aria-valuenow={progress}
              aria-valuemin="0"
              aria-valuemax="100"
              style={{ width: progress + "%" }}
            >
              {progress}%
            </div>
          </div>
        )}

        <label className="btn btn-default">
          <input type="file" onChange={this.selectFile} />
        </label>

        <button className="btn btn-success"
          disabled={!selectedFiles}
          onClick={this.upload}
        >
          Upload
        </button>

        <div className="alert alert-light" role="alert">
          {message}
        </div>

        {/* <div className="card">
          <div className="card-header">List of Files</div>
          <ul className="list-group list-group-flush">
            {fileInfos &&
              fileInfos.map((file, index) => (
                <li className="list-group-item" key={index}>
                  <a href={file.filePath}>{file.fileName}</a>
                </li>
              ))}
          </ul>
        </div> */}
      </div>
    );
  }
}
