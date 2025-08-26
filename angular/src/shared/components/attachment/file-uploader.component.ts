import {
  Component,
  EventEmitter,
  Injector,
  Input,
  Output,
  OnChanges,
  SimpleChanges, // <-- add this
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { FileParameter } from "@shared/service-proxies/service-proxies";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";

@Component({
  selector: "app-file-uploader",
  templateUrl: "./file-uploader.component.html",
  styleUrls: ["./file-uploader.component.scss"],
})
export class FileUploaderComponent extends AppComponentBase {
  @Output() fileSelected = new EventEmitter<{
    files: Array<File & { objectURL?: SafeUrl }>;
  }>();
  @Input() imagePreview: string | null = null;
  isDragging = false;
  selectedFile: (File & { objectURL?: SafeUrl }) | null = null;
  uploadProgress = 0;

  constructor(injector: Injector, private sanitizer: DomSanitizer) {
    super(injector);
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;

    const files = event.dataTransfer?.files;
    if (files?.length) {
      this.handleFile(files[0]);
    }
  }

  onFileSelected(event: any): void {
    const file = event?.target?.files?.[0];
    if (file) {
      this.handleFile(file);
    }
  }

  handleFile(file: File): void {
    if (file.size > 5 * 1024 * 1024) {
      this.notify.error(this.l("File size should not exceed 5MB"));
      return;
    }

    if (!file.type.startsWith("image/")) {
      this.notify.error(this.l("Please upload an image file"));
      return;
    }

    const objectUrl = URL.createObjectURL(file);
    const safeUrl = this.sanitizer.bypassSecurityTrustUrl(objectUrl);

    const enhancedFile = Object.assign(file, {
      objectURL: safeUrl,
    });

    this.selectedFile = enhancedFile;

    const reader = new FileReader();
    reader.onload = (e: ProgressEvent<FileReader>) => {
      this.imagePreview = e.target?.result as string;
      this.simulateUploadProgress();
      this.fileSelected.emit({ files: [enhancedFile] });
    };
    reader.readAsDataURL(file);
  }

  removeFile(): void {
    if (this.selectedFile && this.selectedFile.objectURL) {
      URL.revokeObjectURL(
        (this.selectedFile.objectURL as any)
          .changingThisBreaksApplicationSecurity
      );
    }
    this.selectedFile = null;
    this.imagePreview = null;
    this.uploadProgress = 0;
  }

  private simulateUploadProgress(): void {
    this.uploadProgress = 0;
    const interval = setInterval(() => {
      this.uploadProgress += 10;
      if (this.uploadProgress >= 100) {
        clearInterval(interval);
      }
    }, 1200);
  }

  ngOnDestroy(): void {
    if (this.selectedFile && this.selectedFile.objectURL) {
      URL.revokeObjectURL(
        (this.selectedFile.objectURL as any)
          .changingThisBreaksApplicationSecurity
      );
    }
  }
}
