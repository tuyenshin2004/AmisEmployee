<template>
      <div class="form-process">
            <div class="vertical-center flex">
                  <div class="process-and-dictionary">
                        <div class="process-area">
                              <div class="form-process__title">
                                    NGHIỆP VỤ KHO
                              </div>
                              <div class="content">
                                    <div class="content__inventory mi-h-340">
                                          <base-faceButton
                                                v-for="item in faceButtons"
                                                :key="item.title"
                                                :title="item.title"
                                                :className="item.className"
                                                @click="
                                                      faceButtonOnclick(
                                                            item.title
                                                      )
                                                "
                                          />
                                    </div>
                              </div>
                        </div>
                        <div class="dictionary-area">
                              <div
                                    class="dictionary-item flex-grow"
                                    v-for="item in dictionaryItems"
                                    :key="item.iconItem"
                              >
                                    <div>
                                          <base-icon
                                                :name="item.iconItem"
                                                size="icon--24"
                                          />
                                          <div>{{ item.nameItem }}</div>
                                    </div>
                              </div>
                        </div>
                  </div>
                  <div class="report-area">
                        <div class="form-process__title">BÁO CÁO</div>
                        <div class="report__content">
                              <ul>
                                    <li
                                          v-for="report in reports"
                                          :key="report.content"
                                    >
                                          {{ report.content }}
                                    </li>
                              </ul>
                        </div>
                        <div class="report__footer">Tất cả báo cáo</div>
                  </div>
            </div>
      </div>
      <receipt-detail />
</template>
<script>
      import BaseFaceButton from "../../../base/BaseFaceButton.vue";
      import BaseIcon from "../../../base/BaseIcon.vue";
      import ReceiptDetail from "./detail/ReceiptDetail.vue";
      export default {
            components: {
                  BaseFaceButton,
                  BaseIcon,
                  ReceiptDetail,
            },
            data() {
                  return {
                        faceButtons: [
                              {
                                    title: "Lệnh sản xuất",
                                    className: "production-order",
                              },
                              {
                                    title: "Lắp ráp, tháo rỡ",
                                    className: "assemble-dismantling-order",
                              },
                              {
                                    title: "Xuất kho",
                                    className: "export-inventory",
                              },
                              {
                                    title: "Chuyển kho",
                                    className: "transfer",
                              },
                              {
                                    title: "Nhập kho",
                                    className: "in-inward",
                              },
                              {
                                    title: "Tính giá xuất kho",
                                    className: "outward-price",
                              },
                              {
                                    title: "Kiểm kê",
                                    className: "audit",
                              },
                        ],
                        dictionaryItems: [
                              {
                                    iconItem: "icon__process-stock",
                                    nameItem: "Kho",
                              },
                              {
                                    iconItem: "icon__process-inventory-item",
                                    nameItem: "Vật tư hàng hoá",
                              },
                              {
                                    iconItem: "icon__process-unit-price",
                                    nameItem: "Đơn vị tính",
                              },
                              {
                                    iconItem: "icon__process-tool",
                                    nameItem: "Tiện ích",
                              },
                              {
                                    iconItem: "icon__setting-active",
                                    nameItem: "Tuỳ chọn",
                              },
                        ],
                        reports: [
                              {
                                    content: "Sổ chi tiết vật tư hàng hóa",
                              },
                              {
                                    content: "Tổng hợp tồn kho",
                              },
                              {
                                    content: "Báo cáo đối chiếu kho và sổ cái",
                              },
                              {
                                    content: "Báo cáo tiến độ sản xuất",
                              },
                              {
                                    content: "Báo cáo tồn theo chứng từ nhập chưa chuyển kho",
                              },
                        ],
                  };
            },
            methods: {
                  faceButtonOnclick(title) {
                        if (title == "Nhập kho") {
                              alert("Hiện phiếu nhập kho");
                        }
                  },
            },
      };
</script>
<style scoped>
      @import url(../../../../assets/css/page/warehouse/process.css);
      @import url(../../../../assets/css/base/baseAll.css);
</style>