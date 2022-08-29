<template>
      <div class="page-header">
            <div class="page-header__content">
                  <div class="page-header__title">Nhập, xuất kho</div>
                  <div class="padding__mid"></div>
                  <div class="add-button">
                        <div class="guide-tour">
                              <base-icon name="icon__tour" size="icon--24" />
                              <div class="tour-label">Hướng dẫn</div>
                        </div>
                        <base-button
                              btnTitle="Thêm"
                              btnClass="btn__add btn--green"
                              id="btnAddEmployee"
                              @click="btnAddOnClick"
                        />
                        <base-button
                              btnClass="btn__dropdown btn--green"
                              :hasIcon="true"
                              iconName="icon__dropdown--white"
                        />
                  </div>
            </div>
      </div>
      <div class="page-content">
            <div class="page-content__toolbar">
                  <base-button
                        btnTitle="Thực hiện hàng loạt"
                        btnClass="btn__dropdown"
                        :hasIcon="true"
                        iconName="icon__dropdown--black"
                        @click="btnShowDelete"
                  />
                  <base-button
                        v-if="showDelete"
                        btnTitle="Xoá"
                        btnClass="btn__delete"
                        @click="btnBulkDelete"
                  />
                  <div class="padding-mid"></div>
                  <div class="search">
                        <base-icon name="icon__search" />
                        <input
                              class="field-input"
                              id="inputSearch"
                              placeholder="Nhập từ khoá tìm kiếm"
                              @keyup="employeeFilter($event)"
                        />
                        <!-- <base-input inputId="inputSearch" nameClass="input" :hasIcon="true" iconName="icon__search" inputPlace="Tìm kiếm theo Mã, Tên, hoặc Số điện thoại" /> -->
                  </div>
                  <div class="toolbar__icon">
                        <base-tool content="Tải lại trang" />
                        <base-icon
                              class="icon__refresh"
                              size="icon--24"
                              @click="refreshOnClick()"
                        />
                  </div>

                  <div class="toolbar__icon">
                        <base-tool content="Xuất Excel" />
                        <base-icon
                              class="icon__excel"
                              size="icon--24"
                              @click="exportOnclick()"
                        />
                  </div>
                  <div class="toolbar__icon">
                        <base-tool content="Cài đặt" />
                        <base-icon
                              class="icon__setting-list"
                              size="icon--24"
                              @click="settingOnCLick()"
                        />
                  </div>
            </div>
            <!-- table danh sách và thông tin nhân viên -->
            <div class="page-table">
                  <base-table
                        :headers="headers"
                        :bodyData="employees"
                        @rowSelected="rowOnDblClick"
                        @addToastMsg="addToastMsg"
                        @replicationEmployee="functionReplication"
                        @deleteEmployee="functionDeleteEmp"
                        @rowsSelected="functionListSelected"
                        :isDelete="deletedBulk"
                        @deleted="changeDeleteBulk"
                  />
                  <div class="paging">
                        <!-- left -->
                        <div class="paging__left">
                              Tổng số <b>{{ total }}</b> bản ghi
                        </div>
                        <!-- right -->
                        <div class="paging__right">
                              <div class="paging-select">
                                    <base-page-option
                                          :isPaging="true"
                                          @value="employeeOfPage"
                                    />
                              </div>
                              <!-- <BasePaging
                        :currentPage=currentPage
                        @pageChanged=onPageChange
                        :totalPages="Math.ceil(total / numEmployeeOfPage)"
                        :perPage=numEmployeeOfPage
                        :total=total
                        /> -->
                              <vue-awesome-paginate
                                    :total-items="total"
                                    :items-per-page="numEmployeeOfPage"
                                    :max-pages-shown="5"
                                    :current-page="currentPage"
                                    :on-click="onClickHandler"
                                    prev-button-content="Trước"
                                    next-button-content="Sau"
                              />
                        </div>
                  </div>
            </div>
      </div>
      <base-loading
            :isShow="isShowLoading"
            @isShowLoading="functionIsShowLoading"
      />
      <base-toast />
      <base-dialog
            :isShow="isShowDialog"
            :messages="msgDialog"
            @isShowDialog="functionIsShowDialog"
            iconName="icon__warm"
            :btnLeft="true"
            btnLeftName="Không"
            :btnRight="true"
            btnRight2="Có"
            @btnClick="btnDialogClick"
      />
</template>
<script>
      import BaseButton from "../../../base/BaseButton.vue";
      import BaseIcon from "../../../base/BaseIcon.vue";
      import BasePageOption from "../../../base/BasePageOption.vue";
      import BaseLoading from "../../../base/BaseLoading.vue";
      import BaseTable from "../../../base/BaseTable.vue";
      import BaseToast from "../../../base/BaseToast.vue";
      import BaseDialog from "../../../base/BaseDialog.vue";
      import BaseTool from "../../../base/BaseTool.vue";

      export default {
            components: {
                  BaseButton,
                  BaseIcon,
                  BasePageOption,
                  BaseLoading,
                  BaseTable,
                  BaseToast,
                  BaseDialog,
                  BaseTool,
            },
      };
</script>
<style scoped>
      @import url(../../../../assets/css/page/warehouse/wareList.css);
</style>