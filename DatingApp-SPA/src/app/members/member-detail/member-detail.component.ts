import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { error } from 'protractor';
import {
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation,
} from 'ngx-gallery-9';
import { TabsetComponent } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', { static: true }) memberTabs: TabsetComponent;
  user: User; // Spa User
  galleryOptions: NgxGalleryOptions[]; // 94
  galleryImages: NgxGalleryImage[];

  constructor(
    private alertify: AlertifyService,
    private userService: UserService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.user = data['user']; // ovoj user e od routes.ts pod /members/:id // 93// *
    });

    this.route.queryParams.subscribe((params) => {
      const selectedTab = params['tab'];
      this.memberTabs.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
    });

    // tuka nadole e za Gallery 94

    this.galleryOptions = [
      {
        width: '600px',
        height: '400px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
      },
    ];
    this.galleryImages = this.getImages();

    // this.loadUser();
  }

  getImages() {
    const imageUrls = [];
    for (const photo of this.user.photos) {
      // * //  this.user e zemen od DB preku resolverot so pomos na {{id}} vo URL

      imageUrls.push({
        // ova e samo za Display golemite i malite sliki so ke gi pokazuva html galeryto od ngx

        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description,
      });
    }
    return imageUrls;
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  // loadUser(){
  //   // getUser vrakja observable zatoa mora subscribe // + kaj this e za Int da vrati od Id
  //   this.userService.getUser(+this.route.snapshot.params['id']).subscribe((user: User) => { // 90
  //     this.user = user;
  //   // tslint:disable-next-line: no-shadowed-variable
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }
}
